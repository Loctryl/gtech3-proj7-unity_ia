using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class TilemapVisulazer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilmap, wallTilemap, objectTilmap, TravelTilemap;
    [SerializeField]
    private TileBase SpawnPointTile, ExitPointTile, floorTile, wallTop, wallSideRight, wallSideLeft, wallBottom, wallFull, wallInnerCornnerDownLeft,
        wallInnerCornnerDownRight, wallDiagonalCornerDownRight, wallDiagonalCornnerDownLeft,
        wallDiagonalUpRight, wallDiagonalUpLeft, objetTile1, objectTile2;
    

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilmap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSinlgleTile(tilemap, tile, position);
        }
    }

    private void PaintSinlgleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);    
    }

    public void PaintSingleBasicWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if (WallTypesHelper.wallTop.Contains(typeAsInt))
        {
            tile = wallTop;
        }
        else if (WallTypesHelper.wallSideRight.Contains(typeAsInt))
        {
            tile = wallSideRight;
        }
        else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt))
        {
            tile = wallSideLeft;
        }
        else if (WallTypesHelper.wallBottm.Contains(typeAsInt))
        {
            tile = wallBottom;
        }
        else if (WallTypesHelper.wallFull.Contains(typeAsInt))
        {
            tile = wallFull;
        }
        if (tile != null)
        {
            PaintSinlgleTile(wallTilemap, tile, position);
        }
    }

    public void Clear()
    {
        objectTilmap.ClearAllTiles();
        floorTilmap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
        TravelTilemap.ClearAllTiles();
    }

    internal void PaintSingleCornerWall(Vector2Int position, string bynaryType)
    {
        int typeAsInt = Convert.ToInt32(bynaryType, 2);
        TileBase tile = null;
        if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt))
        {
            tile = wallInnerCornnerDownRight;
        }
        else if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallInnerCornnerDownLeft;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            tile = wallDiagonalUpRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalUpLeft;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornnerDownLeft;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownRight;
        }
        else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt))
        {
            tile = wallFull;
        }
        else if (WallTypesHelper.wallBottmEightDirections.Contains(typeAsInt))
        {
            tile = wallBottom;
        }
        if (tile != null)
        {
            PaintSinlgleTile(wallTilemap, tile, position);
        }
    }

    internal void PaintSingleObject(HashSet<Vector2Int> floorPositions, List<BoundsInt> Roomlist)
    {
        var rngNb = Random.Range(0, floorPositions.Count);
        var position = floorPositions.ElementAt(rngNb);
        int wichObject = Random.Range(0, 1);
        foreach (var room in Roomlist)
        {
            for (int i = 0; i < 3; i++)
            {
                switch (wichObject)
                {
                    case 0: 
                        PaintSinlgleTile(objectTilmap, objetTile1, position);
                        break;
                        case 1:
                            PaintSinlgleTile(objectTilmap, objectTile2, position);
                        break;
                    default:
                        break;
                }
                wichObject = Random.Range(0, 1);
                rngNb = Random.Range(0, floorPositions.Count);
                position = floorPositions.ElementAt(rngNb);
               
            }

        }
        
    }

    internal void PaintSpawnPoint(Vector2Int spawnPoint)
    {
        
       PaintSinlgleTile(TravelTilemap, SpawnPointTile, spawnPoint);

    }

    internal void PaintExitPoint(Vector2Int exitPoint)
    {
        PaintSinlgleTile(TravelTilemap, ExitPointTile, exitPoint);
    }
}
