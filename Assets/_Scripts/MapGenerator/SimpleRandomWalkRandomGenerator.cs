using NavMeshPlus.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{

    [SerializeField]
    protected SimpleRandomWalkSO randomWalkParameters;
    [SerializeField]
    private GameObject boss;

     private GameObject player;
    private GameObject Follower;
    private GameObject navMesh;



    private void Start()
    {
         player = GameObject.FindWithTag("Player");
         Follower = GameObject.FindWithTag("Follower");
         navMesh = GameObject.FindWithTag("NavMesh");
    }

    public override  void RunProceduralGeneration()
    {
        GameObject spawningBoss;
        Transform EnemiesParent = GameObject.Find("enemies").transform;

        var roomList = ProcduralGenration.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, new Vector3Int
           (50 , 50, 0)), 10, 10);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomList)
        {
            roomCenters.Add((Vector2Int)Vector2Int.RoundToInt(room.center));
        }
        var spawnPoint = new Vector2Int(0,0);
       
        var ExitPoint = new Vector2Int(0,0);
        


        HashSet<Vector2Int> floorPosition = RunRandomWalk(randomWalkParameters, startPosition);
        tilemapVisulazer.Clear();
        tilemapVisulazer.PaintFloorTiles(floorPosition);
        WallGenerator.CreateWalls(floorPosition, tilemapVisulazer);
        navMesh.GetComponent<NavMeshSurface>().BuildNavMesh();
        //ItemGenerator.CreateObject(tilemapVisulazer, floor, roomList);
        ItemGenerator.CreateSpawnPoint(tilemapVisulazer, spawnPoint);

        //ItemGenerator.CreateExitPoint(tilemapVisulazer, ExitPoint);


        player.transform.position = new Vector3(spawnPoint.x + 0.56f, spawnPoint.y + 0.56f, 20);
        Follower.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        Follower.GetComponent<Chaser>().target = player.transform;
        Follower.transform.position = new Vector3(spawnPoint.x + 0.56f, spawnPoint.y + 0.56f, 20);
        Follower.gameObject.GetComponent<NavMeshAgent>().enabled = true;
        spawningBoss = Instantiate(boss, new Vector3(0, 0, 20), Quaternion.identity, EnemiesParent);
        spawningBoss.GetComponentInChildren<BehaviourTreeRunner>().player = player;
        spawningBoss.GetComponentInChildren<BehaviourTreeRunner>().enabled = true;
    }

    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO parameters, Vector2Int position)
    {
        var currentPosition = position;
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
