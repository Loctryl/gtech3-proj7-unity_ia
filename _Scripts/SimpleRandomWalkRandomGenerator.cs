using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{

    [SerializeField]
    private SimpleRandomWalkSO randomWalkParameters;



    protected override  void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPosition = RunRandomWalk(randomWalkParameters);
        tilemapVisulazer.Clear();
        tilemapVisulazer.PaintFloorTiles(floorPosition);
        WallGenerator.CreateWalls(floorPosition, tilemapVisulazer);
    }

    private HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO parameters)
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> floorPosition = new HashSet<Vector2Int>();
        for (int i = 0; i < parameters.iterations; i++)
        {
            var path = ProcduralGenration.SimpleRandomWalk(currentPosition, parameters.walklength);
            floorPosition.UnionWith(path);
            if (parameters.startRandomEachIteration)
            {
                currentPosition = floorPosition.ElementAt(Random.Range(0, floorPosition.Count));
            }
        }
        return floorPosition;
    }
}
