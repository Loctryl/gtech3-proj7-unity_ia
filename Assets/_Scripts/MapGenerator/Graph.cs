using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph 
{
    private static List<Vector2Int> neighbour4Direction = new List<Vector2Int>()
    {
        new Vector2Int(0, 1),
        new Vector2Int(1, 0),
        new Vector2Int(0, -1),
        new Vector2Int(-1, 0)
    };

    private static List<Vector2Int> neighbour8Direction = new List<Vector2Int>()
    {
        new Vector2Int(0, 1),
        new Vector2Int(1, 1),
        new Vector2Int(1, 0),
        new Vector2Int(1, -1),
        new Vector2Int(0, -1),
        new Vector2Int(-1, -1),
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 1)
    };

    List<Vector2Int> graph;

    public Graph(IEnumerable<Vector2Int> Vertices)
    {
        graph = new List<Vector2Int>(Vertices);
    }

    public List<Vector2Int> GetNeighbours4Direction(Vector2Int startposition)
    {
        return GetNeighbours(startposition, neighbour4Direction);
    }
    public List<Vector2Int> GetNeighbours8Direction(Vector2Int startposition)
    {
        return GetNeighbours(startposition, neighbour8Direction);
    }

    private List<Vector2Int> GetNeighbours(Vector2Int startposition, List<Vector2Int> neighboursOffsetList)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>();
        foreach (var neighboursDirection in neighboursOffsetList)
        {
            Vector2Int neighbour = startposition + neighboursDirection;
            if (graph.Contains(neighbour))
            {
                neighbours.Add(neighbour);
            }
        }
        return neighbours;
    }
}
