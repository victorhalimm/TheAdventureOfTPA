using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>
{
    public bool isWalkable;
    public Vector3 pos;

    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;

    public Node parent;

    int heapIndex;

    public Node(bool isWalkable, Vector3 pos, int gridX, int gridY)
    {
        this.isWalkable = isWalkable;
        this.pos = pos;
        this.gridX = gridX;
        this.gridY = gridY;
    }

    public int fCost
    {
        get
        {
            return hCost + gCost;
        }
    }

    public int heapIdx
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }


    public int CompareTo(Node nodeToCompare)
    {
        int compareVal = fCost.CompareTo(nodeToCompare.fCost);
        // kalo f cost sama compare h costnya lagi (dilakuin disini biar nanti gaperlu tulis panjang" lagi)
        if (compareVal == 0)
        {
            compareVal = hCost.CompareTo(nodeToCompare.hCost);
        }
        return compareVal;
    }
}
