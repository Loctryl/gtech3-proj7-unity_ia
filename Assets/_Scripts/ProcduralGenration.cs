using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidht, int minHeight)
    {
        Queue<BoundsInt> roomQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomList = new List<BoundsInt>();
        roomQueue.Enqueue(spaceToSplit);
        while (roomQueue.Count > 0)
        {
            var room = roomQueue.Dequeue();
            if (room.size.y >= minHeight && room.size.x >= minWidht)
            {
                if (Random.value < 0.5f)
                {
                    if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomQueue, room);
                    }
                    else if (room.size.x >= minWidht * 2)
                    {
                        SplitVertically(minWidht, roomQueue, room);
                    }
                    else if (room.size.x >= minWidht && room.size.y >= minHeight)
                    {
                        roomList.Add(room);
                    }
                }
                else
                {
                    if (room.size.x >= minWidht * 2)
                    {
                        SplitVertically(minHeight, roomQueue, room);
                    }
                    else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minWidht, roomQueue, room);
                    }
                    else if (room.size.x >= minWidht && room.size.y >= minHeight)
                    {
                        roomList.Add(room);
                    }
                }
            }
        }
        return roomList;
    } 

    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
        roomQueue.Enqueue(room1);
        roomQueue.Enqueue(room2);
    }

    private static void SplitVertically(int minWidht, Queue<BoundsInt> roomQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomQueue.Enqueue(room1);
        roomQueue.Enqueue(room2);
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