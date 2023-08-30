using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] swordModels;
    [SerializeField] private Terrain terrain;
    [SerializeField] private int swordCount;

    private List<BoundsInt> bspRoom;

    private bool[,] visitedPath;
    private int[,] primMap;

    private int pathCount = 0;
    public void DFSTraverse(int startX, int startY)
    {
        DFS(startX, startY, primMap.GetLength(0), primMap.GetLength(1));
    }

    public void DFS(int x, int y, int row, int column)
    {
 
        if (x >= 0 && y >= 0 && x < row && y < column && !visitedPath[x, y] && primMap[x, y] == 0)
        {
            
            visitedPath[x, y] = true;
            pathCount++;
            if (UnityEngine.Random.value > 0.95f)
            {
                bool inARoom = false;
                Vector3 pos = new Vector3(x * 1f, 0, y * 1f);
                foreach(BoundsInt room in bspRoom)
                {
                    if (room.xMin <= pos.x && room.xMax >= pos.x && room.zMin <= pos.z && room.zMax >= pos.z)
                    {
                        inARoom = true;
                        break;
                    }
                }
                if (inARoom == true)
                {
                    float yWorld = terrain.SampleHeight(pos) + 1.1f;
                    GameObject sword = Instantiate(swordModels[Random.Range(0, swordModels.Length - 1)], new Vector3(pos.x, yWorld, pos.z), Quaternion.Euler(Random.Range(-150f, -210f), 0, 0));

                    sword.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                }
            }

            DFS(x + 1, y, row, column);
            DFS(x - 1, y, row, column); 
            DFS(x, y + 1, row, column); 
            DFS(x, y - 1, row, column); 
        }
    }


    void Start()
    {
        primMap = Prim.instance.map;
        bspRoom = BSP.instance.rooms;
        visitedPath = new bool[primMap.GetLength(0), primMap.GetLength(1)];
        DFSTraverse(0, 0);
        
    }
}
