using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class TilemapVisulazer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilmap, wallTilemap;
    [SerializeField]
    private TileBase floorTile, wallTop;

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

    public void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSinlgleTile(wallTilemap, wallTop, position);
    }

    public void Clear()
    {
        floorTilmap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }
}
