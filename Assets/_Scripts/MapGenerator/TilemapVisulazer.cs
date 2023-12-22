using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

using Random = UnityEngine.Random;

public class TilemapVisulazer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilmap, wallTilemap, objectTilmap, TravelSpawn, TravelExit;
    [SerializeField]
    private TileBase SpawnPointTile, ExitPointTile,
        floorTile1, floorTile2, floorTile3, floorTile4, floorTile5, floorTile6, floorTile7, floorTile8, floorTile9, floorTile10, floorTile11, floorTile12, floorTile13, floorTile14,
        wallTop, wallSideRight, wallSideLeft, wallBottom, wallFull, wallInnerCornnerDownLeft,
        wallInnerCornnerDownRight, wallDiagonalCornerDownRight, wallDiagonalCornnerDownLeft,
        wallDiagonalUpRight, wallDiagonalUpLeft,
        Deco1, Deco2, Deco3, Deco4, Deco5, Deco6, ObjectLightTile;
    [SerializeField]
    private GameObject Light;
    [SerializeField]
    private GameObject Cobweb;
    [SerializeField]
    private GameObject Enemy1;
    [SerializeField]
    private GameObject Enemy2;
    [SerializeField]
    private GameObject Enemy3;
    [SerializeField]
    private GameObject ChestMimic;


    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilmap);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap)
    {
        int rngNb;
        TileBase[] floorTile = new TileBase[14] { floorTile1, floorTile2, floorTile3, floorTile4, floorTile5, floorTile6, floorTile7, floorTile8, floorTile9, floorTile10, floorTile11, floorTile12, floorTile13, floorTile14 };

        foreach (var position in positions)
        {
            rngNb = Random.Range(0, 13);
            PaintSinlgleTile(tilemap, floorTile[rngNb], position);
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
        GameObject[] objToDestroy;
        objToDestroy = GameObject.FindGameObjectsWithTag("Light");
        foreach (var item in objToDestroy)
        {
            Destroy(item);
        }
        objToDestroy = GameObject.FindGameObjectsWithTag("Enemies");
        foreach (var item in objToDestroy)
        {
            Destroy(item);
        }




        objectTilmap.ClearAllTiles();
        floorTilmap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
        TravelExit.ClearAllTiles();
        TravelSpawn.ClearAllTiles();
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
        int wichObject = Random.Range(0, 7);
        GameObject light;





        foreach (var room in Roomlist)
        {
            for (int i = 0; i < 5; i++)
            {
                wichObject = Random.Range(0, 4);

                switch (wichObject)
                {
                    case 0:
                        PaintSinlgleTile(objectTilmap, Deco1, position);
                        break;
                    case 1:
                        PaintSinlgleTile(objectTilmap, Deco2, position);
                        break;
                    case 2:
                        PaintSinlgleTile(objectTilmap, Deco3, position);
                        break;
                    case 3:
                        PaintSinlgleTile(objectTilmap, ObjectLightTile, position);
                        light = Instantiate(Light, new Vector3(position.x + 0.56f, position.y + 0.56f, 15), Quaternion.identity); break;
                    case 4:
                        PaintSinlgleTile(objectTilmap, Deco5, position);
                        break;
                    case 5:
                        PaintSinlgleTile(objectTilmap, Deco6, position);
                        break;
                    case 6:
                        PaintSinlgleTile(objectTilmap, ObjectLightTile, position);
                        light = Instantiate(Light, new Vector3(position.x + 0.56f, position.y + 0.56f, 15), Quaternion.identity);

                        break;

                    default:
                        break;
                }
                rngNb = Random.Range(0, floorPositions.Count);
                position = floorPositions.ElementAt(rngNb);



            }

        }

    }

    internal void PaintSpawnPoint(Vector2Int spawnPoint)
    {

        PaintSinlgleTile(TravelSpawn, SpawnPointTile, spawnPoint);

    }

    internal void PaintExitPoint(Vector2Int exitPoint)
    {
        PaintSinlgleTile(TravelExit, ExitPointTile, exitPoint);
    }

    internal void PaintEnemiesSpawnPoint(HashSet<Vector2Int> floorPositions, List<BoundsInt> roomList)
    {
        var rngNb = Random.Range(0, floorPositions.Count);
        var position = floorPositions.ElementAt(rngNb);
        int wichObject = Random.Range(0, 4);
        GameObject Enemie;
        bool isChestSpawned = false;

        Transform EnemiesParent = GameObject.Find("enemies").transform;


        foreach (var room in roomList)
        {
            wichObject = Random.Range(0, 5);

            for (int i = 0; i < 3; i++)
            {

                switch (wichObject)
                {
                    case 0:
                        Enemie = Instantiate(Cobweb, new Vector3(position.x + 0.56f, position.y + 0.56f, 20), Quaternion.identity);
                        break;
                    case 1:
                        Enemie = Instantiate(Enemy1, new Vector3(position.x + 0.56f, position.y + 0.56f, 20), Quaternion.identity, EnemiesParent);
                        break;
                    case 2:
                        Enemie = Instantiate(Enemy2, new Vector3(position.x + 0.56f, position.y + 0.56f, 20), Quaternion.identity, EnemiesParent);
                        break;
                    case 3:
                        if (isChestSpawned == false)
                        {
                            Enemie = Instantiate(ChestMimic, new Vector3(position.x + 0.56f, position.y + 0.56f, 20), Quaternion.identity, EnemiesParent);
                            isChestSpawned = true;
                        }
                        break;
                    case 4:
                        Enemie = Instantiate(Enemy3, new Vector3(position.x + 0.56f, position.y + 0.56f, 20), Quaternion.identity, EnemiesParent);
                        break;
                    default:
                        break;
                }
                rngNb = Random.Range(0, floorPositions.Count);
                position = floorPositions.ElementAt(rngNb);
            }
        }
    }
}