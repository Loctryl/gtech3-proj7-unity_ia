using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    protected override void RunProceduralGeneration()
    {
        CreateRoom();
    }

    private void CreateRoom()
    {
        var  roomList = ProcduralGenration.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, new Vector3Int
            (dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        floor = CreateSimpleRoom(roomList);

        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomList)
        {
            roomCenters.Add((Vector2Int)Vector2Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters, floor);
        floor.UnionWith(corridors);

        tilemapVisulazer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tilemapVisulazer);
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters, HashSet<Vector2Int> floor)
    {

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
