using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathFinding : MonoBehaviour
{
    Grid grid;

    PathRequestManager pathRequestManager;

    private void Awake()
    {
        grid = GetComponent<Grid>();
        pathRequestManager = GetComponent<PathRequestManager>();
    }

    public void startFindPath(Vector3 startPos, Vector3 endPos)
    {
        StartCoroutine(findPath(startPos, endPos));
    }

    IEnumerator findPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        Vector3[] waypoints = new Vector3[0];
        bool pathFound = false;

        if (targetNode.isWalkable)
        {
            Heap<Node> openSet = new Heap<Node>(grid.maxSize);
            HashSet<Node> closeSet = new HashSet<Node>();


            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.removeFirst();
                closeSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    pathFound = true;
                    break;
                }

                foreach (Node neighbour in grid.getNeighbours(currentNode))
                {
                    if (!neighbour.isWalkable || closeSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newCostToNeighbour = currentNode.gCost + getDistance(currentNode, neighbour);

                    if (!openSet.Contains(neighbour) || newCostToNeighbour < neighbour.gCost)
                    {
                        neighbour.gCost = newCostToNeighbour;
                        neighbour.hCost = getDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour)) openSet.Add(neighbour);
                        else openSet.updateHeap(neighbour);
                    }
                }
            }

        }
        yield return null;
        if (pathFound) waypoints = retracePath(startNode, targetNode);
        pathRequestManager.FinishedProcessPath(waypoints, pathFound);
    }

    Vector3[] retracePath(Node startNode, Node targetNode)
    {
        List<Node> path = new List<Node>();

        Node curr = targetNode;

        while(curr != startNode)
        {
            path.Add(curr);
            curr = curr.parent;
        }
        Vector3[] waypoints = simplifyPath(path);
        Array.Reverse(waypoints);

        return waypoints;
    }

    Vector3[] simplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 oldDirection = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 newDirection = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);

            if (newDirection != oldDirection)
            {
                waypoints.Add(path[i].pos);
                oldDirection = newDirection;
            }
        }
        return waypoints.ToArray();
    }

    int getDistance(Node a, Node b)
    {
        int disX = Mathf.Abs(a.gridX - b.gridX);
        int disY = Mathf.Abs(a.gridY - b.gridY);

        if (disX > disY)
        {
            return 14 * disY + 10 * (disX - disY);
        }

        return 14 * disX + 10 * (disY - disX);
    }
}
