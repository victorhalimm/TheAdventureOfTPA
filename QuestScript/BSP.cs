using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSP : MonoBehaviour
{
    public List<BoundsInt> rooms;

    public static BSP instance;

    private void Awake()
    {
        BoundsInt terrainBounds = new BoundsInt(0, 0, 0, 250, 0, 250);
        rooms = BinarySpacePartitioning(terrainBounds, 10, 10);
        instance = this;
    }

    //Buat nanti presentasi uncomment ya

    void OnDrawGizmos()
    {

        if (rooms != null)
        {
            foreach (var room in rooms)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawCube(new Vector3(room.center.x, 0, room.center.z), new Vector3(room.size.x, 1, room.size.z));
            }
        }
    }
    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit);

        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();

            if (room.size.x >= minWidth && room.size.z >= minHeight)
            {
                if (UnityEngine.Random.value > 0.5f)
                {
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitHorizontally(roomsQueue, room);
                    }

                    else if (room.size.z >= minHeight * 2)
                    {
                        SplitVertically(roomsQueue, room);
                    }

                    else roomsList.Add(room);

                }
                else
                {
                    if (room.size.z >= minHeight * 2)
                    {
                        SplitVertically(roomsQueue, room);
                    }

                    else if (room.size.x >= minWidth * 2)
                    {
                        SplitHorizontally(roomsQueue, room);
                    }

                    else roomsList.Add(room);
                }
            }
        }
        return roomsList;
    }

    private static void SplitHorizontally(Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = UnityEngine.Random.Range(1, room.size.x);
        BoundsInt roomLeft = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt roomRight = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z), new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomsQueue.Enqueue(roomLeft);
        roomsQueue.Enqueue(roomRight);
    }

    private static void SplitVertically(Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var zSplit = UnityEngine.Random.Range(1, room.size.z);
        BoundsInt roomTop = new BoundsInt(room.min, new Vector3Int(room.size.x, room.size.y, zSplit));
        BoundsInt roomBottom = new BoundsInt(new Vector3Int(room.min.x, room.min.y, room.min.z + zSplit), new Vector3Int(room.size.x, room.size.y, room.size.z - zSplit));
        roomsQueue.Enqueue(roomTop);
        roomsQueue.Enqueue(roomBottom);
    }
}
