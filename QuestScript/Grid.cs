using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public LayerMask obstacle;
    public Vector2 gridSize;
    public float nodeRadius;

    public bool displayGridGizmos;

    Node[,] grid;
    float nodeDiameter;
    int gridXCount, gridYCount;

    public List<Node> path;
    private void Start()
    {
        // tentuin jumlah node pada grid di x sama y
        nodeDiameter = nodeRadius * 2;
        gridXCount = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridYCount = Mathf.RoundToInt(gridSize.y / nodeDiameter);
        CreateGrid();
    }

    public int maxSize
    {
        get
        {
            return gridXCount * gridYCount;
        }
    }


    void CreateGrid()
    {
        
        grid = new Node[gridXCount, gridYCount];

        Vector3 worldBottomLeft = transform.position - Vector3.right * (gridXCount / 2 * nodeDiameter) - Vector3.forward * (gridYCount / 2 * nodeDiameter);

        for (int i = 0; i < gridXCount; i++)
        {
            for (int j = 0; j < gridYCount; j++)
            {
                //Ngedapetin posisi titik-titik dari seluruh terrain berdasarkan diameter dan radius yang ditentukan
                Vector3 nodePos = worldBottomLeft + Vector3.right * (i * nodeDiameter + nodeRadius) + Vector3.forward * (j * nodeDiameter + nodeRadius);

                nodePos.y = Terrain.activeTerrain.SampleHeight(nodePos);
                // Ngecek apakah di suatu titik ada objek ato ga, kalo ada brarti not walkable
                bool isWalkable = !Physics.CheckSphere(nodePos, nodeRadius, obstacle);
                grid[i, j] = new Node(isWalkable, nodePos, i, j); 
            }
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        Vector3 localPosition = worldPosition - transform.position;
        float percentX = Mathf.Clamp01(localPosition.x / gridSize.x + 0.5f);
        float percentY = Mathf.Clamp01(localPosition.z / gridSize.y + 0.5f);

        int x = Mathf.FloorToInt(Mathf.Clamp01(percentX) * (gridXCount - 1));
        int y = Mathf.FloorToInt(Mathf.Clamp01(percentY) * (gridYCount - 1));
        return grid[x, y];
    }

    public List<Node> getNeighbours(Node curr)
    {
        List<Node> neighbours = new List<Node>();

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                // Kalo misalnya dia ngecek node diri dia sendiri skip jan taro ke neighbour
                if (i == 0 && j == 0)
                {
                    continue;
                }

                int xPos = curr.gridX + i;
                int yPos = curr.gridY + j;

                if (xPos >= 0 && xPos < gridXCount && yPos >= 0 && yPos < gridYCount)
                {
                    neighbours.Add(grid[xPos, yPos]);
                }
            }
        }

        return neighbours;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, 1, gridSize.y));
        if (grid != null && displayGridGizmos)
        {
            foreach (Node node in grid)
            {
                Gizmos.color = (node.isWalkable) ? Color.white : Color.red;
                Gizmos.DrawCube(node.pos, Vector3.one * (nodeDiameter - 0.2f));
            }
        }
    }

}
