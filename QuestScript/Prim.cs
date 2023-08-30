using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prim : MonoBehaviour
{
    public int height;
    public int width;
    public int wall;
    public float cellSize;

    private List<Vector2Int> maze;
    private int[,] visited;
    public int[,] map;

    public static Prim instance;

    private void Awake()
    {
        instance = this;
        maze = new List<Vector2Int>();
        visited = new int[width, height];
        map = new int[width, height];
        // set semua jadi dinding terlebih dahulu
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                map[i, j] = 1;
            }
        }

        GenerateMazePrim();
    }


    private void GenerateMazePrim()
    {
        int[] xDirection = { 0, 2, 0, -2 };
        int[] yDirection = { -2, 0, 2, 0 };
        // mulai dari titik 0,0 (ujung kiri bawah)

        Vector2Int temp = new Vector2Int(0, 0);
        maze.Add(temp);

        int firstIter = 1;
        int count = 0;

        while (maze.Count > 0)
        {
            Vector2Int curr = maze[0];
            maze.RemoveAt(0);

            if (visited[curr.x, curr.y] == 1)
                continue;

            visited[curr.x, curr.y] = 1;
            map[curr.x, curr.y] = 0;

            int randomNeighbor;
            bool valid;

            if (firstIter == 1)
            {
                firstIter = 0;
            }
            else
            {
                do
                {
                    randomNeighbor = Random.Range(0, 4);
                    if (curr.x - xDirection[randomNeighbor] < 0)
                    {
                        valid = false;
                    }
                    else if (curr.x - xDirection[randomNeighbor] >= width)
                    {
                        valid = false;
                    }
                    else if (curr.y - yDirection[randomNeighbor] < 0)
                    {
                        valid = false;
                    }
                    else if (curr.y - yDirection[randomNeighbor] >= height)
                    {
                        valid = false;
                    }
                    else if (map[curr.x - xDirection[randomNeighbor], curr.y - yDirection[randomNeighbor]] != 0)
                    {
                        valid = false;
                    }
                    else
                    {
                        valid = true;
                    }
                } while (!valid);

                map[curr.x - (xDirection[randomNeighbor] / 2), curr.y - (yDirection[randomNeighbor] / 2)] = 0;
            }

            for (int i = 0; i < 4; i++)
            {
                if (curr.x - xDirection[i] >= 0 && curr.x - xDirection[i] < width && curr.y - yDirection[i] >= 0 && curr.y - yDirection[i] < height)
                {
                    Vector2Int neighbor = new Vector2Int(curr.x - xDirection[i], curr.y - yDirection[i]);
                    maze.Add(neighbor);
                    count++;
                }
            }
        }
    }

    // buat nanti presentasi uncomment ya

    private void OnDrawGizmos()
    {
        if (map != null)
        {
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    if (map[i, j] == 1)
                    {
                        Gizmos.color = Color.black;
                        Gizmos.DrawCube(new Vector3(i * cellSize, 10, j * cellSize), new Vector3(cellSize, wall, cellSize));
                    }
                    else
                    {
                        Gizmos.color = Color.white;
                        Gizmos.DrawCube(new Vector3(i * cellSize, 10, j * cellSize), new Vector3(cellSize, wall, cellSize));
                    }
                }
            }
        }
    }
}