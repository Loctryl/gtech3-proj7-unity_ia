using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimplaeCorridorGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercent = 0.8f;

    private Dictionary<Vector2Int, HashSet<Vector2Int>> roomDictionnary = new Dictionary<Vector2Int, HashSet<Vector2Int>>();

    private HashSet<Vector2Int> floorPositions, corridorPositions;

    private List<Color> roomColors = new List<Color>();
    

    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        List<List<Vector2Int>> corridors = CreateCorridor(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRoom(potentialRoomPositions);
        List<Vector2Int> deadEnd = FindAllDeadEnd(floorPositions);

        CreateRoomDeadEnd(deadEnd, roomPositions);

        floorPositions.UnionWith(roomPositions);

        for (int i = 0; i < corridors.Count; i++)
        {
            corridors[i] = IncreaseCorridorSize(corridors[i]);
            floorPositions.UnionWith(corridors[i]);
        }

        tilemapVisulazer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisulazer);
    }

    public List<Vector2Int> IncreaseCorridorSize(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        Vector2Int previousDirection = Vector2Int.zero;
        for (int i = 1; i < corridor.Count; i++)
        {
            Vector2Int direction = corridor[i] - corridor[i - 1];
            previousDirection = direction;
            if (previousDirection != Vector2Int.zero && direction != previousDirection)
            {
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        newCorridor.Add(corridor[i - 10] + new Vector2Int(x, y));
                    }
                }
                previousDirection = direction;
            }
            else
            {
                Vector2Int newCorridorTileOffset = GetDirection90From(direction);
                newCorridor.Add(corridor[i - 1]);
                newCorridor.Add(corridor[i - 1] + newCorridorTileOffset);
            }
            
        }   
        return corridor;
    }   

    private Vector2Int GetDirection90From(Vector2Int direction)
    {
        if (direction == Vector2Int.up)
        {
            return Vector2Int.right;
        }
        if (direction == Vector2Int.right)
        {
            return Vector2Int.down;
        }
        if (direction == Vector2Int.down)
        {
            return Vector2Int.left;
        }
        if (direction == Vector2Int.left)
        {
            return Vector2Int.up;
        }
        return Vector2Int.zero;
    }

    private void CreateRoomDeadEnd(List<Vector2Int> deadEnd, HashSet<Vector2Int> roomPositions)
    {
        foreach (var position in deadEnd)
        {
            if (roomPositions.Contains(position) == false)
            {
                var room = RunRandomWalk(randomWalkParameters, position);
                roomPositions.UnionWith(room);
            }
        }
    }   
    private List<Vector2Int> FindAllDeadEnd(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnd = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if (floorPositions.Contains(position + direction))
                {
                    neighboursCount++;
                }
            }
            if (neighboursCount == 1)
            {
                deadEnd.Add(position);
            }
        }
        return deadEnd;
    }

    private List<List<Vector2Int>> CreateCorridor(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();
        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProcduralGenration.RandomWalkCorridor(currentPosition, corridorLength);
            corridors.Add(corridor);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);

        }
        corridorPositions = new HashSet<Vector2Int>(floorPositions);
        return corridors;
    }

    private HashSet<Vector2Int> CreateRoom(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        List<Vector2Int> roomToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();
        ClearRoomData();

        foreach (var roomPosition in roomToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            SaveRoomData(roomPosition, roomFloor);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    private void SaveRoomData(Vector2Int roomPosition, HashSet<Vector2Int> roomFloor)
    {
        roomDictionnary[roomPosition] = roomFloor;
        roomColors.Add(UnityEngine.Random.ColorHSV());
    }

    private void ClearRoomData()
    {
       roomDictionnary.Clear();
        roomColors.Clear();
    }
}
