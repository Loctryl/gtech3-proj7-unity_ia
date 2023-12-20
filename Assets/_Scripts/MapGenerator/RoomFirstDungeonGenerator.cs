using System;
using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;
    [SerializeField]
    private int dungeonWidth = 20, dungeonHeight = 20;
    [SerializeField]
    [Range(0, 10)]
    private int offset = 1;
    [SerializeField]
    private bool randomWalkRoom = false;

    [SerializeField]
    GameObject player;

    [SerializeField] 
    private GameObject navMesh;



    private void Start()
    {
        tilemapVisulazer.Clear();
        RunProceduralGeneration();
    }
    public override void RunProceduralGeneration()
    {
        tilemapVisulazer.Clear();
        CreateRoom();
    }

    private void CreateRoom()
    {
        var  roomList = ProcduralGenration.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, new Vector3Int
            (dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        if (randomWalkRoom)
        {
            floor = CreateRoomRandomly(roomList);
        }
        else
        {
            floor = CreateSimpleRoom(roomList);
        }

        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomList)
        {
            roomCenters.Add((Vector2Int)Vector2Int.RoundToInt(room.center));
        }
         var spawnPoint = new Vector2Int();
        spawnPoint = roomCenters[0];
        var ExitPoint = new Vector2Int();
        ExitPoint = roomCenters[roomCenters.Count - 1];
        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters, floor);
        floor.UnionWith(corridors);

        tilemapVisulazer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tilemapVisulazer);
        //ItemGenerator.CreateObject(tilemapVisulazer, floor , roomList);
        ItemGenerator.CreateSpawnPoint(tilemapVisulazer, spawnPoint);
        ItemGenerator.CreateExitPoint(tilemapVisulazer, ExitPoint);
        navMesh.GetComponent<NavMeshSurface>().BuildNavMesh();
        ItemGenerator.CreateEnemiesSpawnPoint(tilemapVisulazer, floor, roomList);
        
        
        player.transform.position = new Vector3(spawnPoint.x + 0.56f, spawnPoint.y + 0.56f, -5);
    }

    private HashSet<Vector2Int> CreateRoomRandomly(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for (int i = 0; i < roomList.Count; i++)
        {
            var roomBounds = roomList[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);
            foreach (var position in roomFloor)
            {
                if (position.x >= roomBounds.xMin + offset && position.x <= (roomBounds.xMax - offset) && position.y >= roomBounds.yMin + offset &&
                    position.y <= (roomBounds.yMax - offset))
                {
                    floor.Add(position);
                }
            }
        }
     
        return floor;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters, HashSet<Vector2Int> floor)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);
        while (roomCenters.Count > 0)
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newcorridor = CreateCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;
            corridors.UnionWith(newcorridor);
        }
        return corridors;   
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = currentRoomCenter;
        corridor.Add(position);
        while (position.y != destination.y)
        {
            if (destination.y > position.y)
            {
                position += Vector2Int.up; 
            }
            else if (destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            corridor.Add(position);
        }
        while (position.x != destination.x)
        {
            if (destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            corridor.Add(position);
        }
        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;
        foreach (var position in roomCenters)
        {
            float currentDistance = Vector2.Distance(position, currentRoomCenter);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closest = position;
            }
        }
        return closest;
    }

    private HashSet<Vector2Int> CreateSimpleRoom(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomList)
        {
            for (int x = offset; x < room.size.x - offset; x++)
            {
                for (int y = offset; y < room.size.y - offset; y++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(x, y);
                    floor.Add(position);
                }
            }
        }
        return floor;
    }
}
