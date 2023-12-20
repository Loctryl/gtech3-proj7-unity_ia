using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  static class ItemGenerator 
{


    public static void CreateObject(TilemapVisulazer tilemapVisualizer, HashSet<Vector2Int> floorPositions, List<BoundsInt> RoomList)
    {
        tilemapVisualizer.PaintSingleObject(floorPositions, RoomList);
    }

    public static void CreateSpawnPoint(TilemapVisulazer tilemapVisualizer, Vector2Int spawnPoint)
    {
        tilemapVisualizer.PaintSpawnPoint(spawnPoint);
    }

    internal static void CreateExitPoint(TilemapVisulazer tilemapVisulazer, Vector2Int exitPoint)
    {
        tilemapVisulazer.PaintExitPoint(exitPoint);
    }

    internal static void CreateEnemiesSpawnPoint(TilemapVisulazer tilemapVisulazer, HashSet<Vector2Int> floorPositions, List<BoundsInt> RoomList)
    {
        tilemapVisulazer.PaintEnemiesSpawnPoint(floorPositions, RoomList);
    }
}
