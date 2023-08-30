using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathRequestManager : MonoBehaviour
{

    Queue<PathRequest> pathReqQueue = new Queue<PathRequest>();
    PathRequest currPathReq;

    public static PathRequestManager instance;

    PathFinding pathFinding;
    bool isProcessing;

    private void Awake()
    {
        instance = this;
        pathFinding = GetComponent<PathFinding>();
    }
    public static void requestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        PathRequest newReq = new PathRequest(pathStart, pathEnd, callback);
        instance.pathReqQueue.Enqueue(newReq);
        instance.TryProcessNext();
    }

    void TryProcessNext()
    {
        if (!isProcessing && pathReqQueue.Count > 0)
        {
            currPathReq = pathReqQueue.Dequeue();
            isProcessing = true;
            pathFinding.startFindPath(currPathReq.pathStart, currPathReq.pathEnd);
        }
    }

    public void FinishedProcessPath(Vector3[] path, bool success)
    {
        currPathReq.callback(path, success);
        isProcessing = false;
        TryProcessNext();
    }

    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;

        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
        {
            this.pathStart = pathStart;
            this.pathEnd = pathEnd;
            this.callback = callback;
        }
    }
}
