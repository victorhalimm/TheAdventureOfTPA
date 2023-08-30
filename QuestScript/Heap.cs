using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Heap<T> where T : IHeapItem<T>
{
    T[] items;
    int itemCount;

    public Heap(int maxCount)
    {
        items = new T[maxCount];
    }

    public void updateHeap(T item)
    {
        heapifyUp(item);
    }
    public int Count
    {
        get
        {
            return itemCount;
        }

    }

    public bool Contains(T item)
    {
        return Equals(items[item.heapIdx], item);
    }

    public void Add(T item)
    {
        item.heapIdx = itemCount;
        items[itemCount] = item;
        heapifyUp(item);
        itemCount++;
    }

    public T removeFirst()
    {
        T firstItem = items[0];
        itemCount--;
        items[0] = items[itemCount];
        items[0].heapIdx = 0;
        heapifyDown(items[0]);
        return firstItem;
    }

    void heapifyDown(T item)
    {
        while (true)
        {
            int rightChildIdx = 2 * item.heapIdx + 2;
            int leftChildIdx = 2 * item.heapIdx + 1;

            int swapIdx = 0;
            if (leftChildIdx < itemCount)
            {
                swapIdx = leftChildIdx;

                if (rightChildIdx < itemCount && items[rightChildIdx].CompareTo(items[leftChildIdx]) < 0)
                {
                    swapIdx = rightChildIdx;
                }

                if (item.CompareTo(items[swapIdx]) > 0)
                {
                    swap(item, items[swapIdx]);
                }
                else return;
            }
            else return;

        }
    }

    void heapifyUp(T item)
    {
        int parentIdx = (item.heapIdx - 1) / 2;
        
        while (true)
        {
            if (item.CompareTo(items[parentIdx]) < 0)
            {
                swap(item, items[parentIdx]);
            }
            else break;

            parentIdx = (item.heapIdx - 1) / 2;
        }
    }
    
    // Kalo di C# gada pass by reference
    void swap(T a, T b)
    {
        items[a.heapIdx] = b;
        items[b.heapIdx] = a;
        int idxA = a.heapIdx;
        a.heapIdx = b.heapIdx;
        b.heapIdx = idxA;
    }
}

// Bikin interface agar item generic yang ada di heap bisa dikasih attribute

public interface IHeapItem<T> : IComparable<T>
{
    int heapIdx
    {
        get;
        set;
    }
}