using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProcduralGenration
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walklenght)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();  

        path.Add(startPosition);
        var previousPosition = startPosition;   

        for (int i = 0; i < walklenght; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return path;
    }   

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startpostion, int corridorLenght)
    {        
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        var currentPostion = startpostion;
        corridor.Add(currentPostion);

        for (int i = 0; i < corridorLenght; i++)
        {
            currentPostion += direction;
            corridor.Add(currentPostion);
        }

        return corridor;
        
     
    }
}

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionList = new List<Vector2Int>()
    {
        new Vector2Int(0,1),
        new Vector2Int(0,-1),
        new Vector2Int(1,0),
        new Vector2Int(-1,0)
    };

    public static Vector2Int GetRandomCardinalDirection()
    {
       return cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)];
    }
}