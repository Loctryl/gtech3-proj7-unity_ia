using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  static class ItemGenerator 
{


    public static void CreateObject(TilemapVisulazer tilemapVisualizer, HashSet<Vector2Int> floorPositions, List<BoundsInt> RoomList)
    {
        tilemapVisualizer.PaintSingleObject(floorPositions, RoomList);
    }
}
