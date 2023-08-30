using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlayer : MonoBehaviour
{

    public Transform target;
    public Animator anim;
    
    public float speed;
    int targetIdx;
    Vector3[] path;
    Vector3 oldTargetPos;

    public float moveInterval;
    float lastMoved;

    private bool arrived = false;

    private void Start()
    {
        lastMoved = 0;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (target.position != oldTargetPos &&  Time.time - lastMoved >= moveInterval )
        {
            PathRequestManager.requestPath(transform.position, target.position, OnPathFound);
            oldTargetPos = target.position;
            lastMoved = Time.time;
        }
        else if (arrived && Vector3.Distance(transform.position, target.position) > 5f)
        {
            arrived = false;
            PathRequestManager.requestPath(transform.position, target.position, OnPathFound);
        }
    }

    //Callback function
    void OnPathFound(Vector3[] newPath, bool found) 
    {
        if (found && newPath != null)
        {
            path = newPath;
            StopCoroutine(FollowPath());
            StartCoroutine(FollowPath());
        }
    }

    IEnumerator FollowPath()
    {
        if (path.Length > 0)
        {
            anim.SetBool("Attack", false);
            anim.SetBool("Walk", true);
            Vector3 currWaypoint = path[0];
            while (true)
            {
                if (Vector3.Distance(transform.position, currWaypoint) <= 1f)
                {
                    targetIdx++;
                    if (targetIdx >= path.Length)
                    {
                        anim.SetBool("Walk", false);
                        anim.SetBool("Attack", true);
                        arrived = true;
                        yield break;
                    }
                    currWaypoint = path[targetIdx];
                }

                Vector3 directionPos = (currWaypoint - transform.position).normalized;
                Quaternion rotation = Quaternion.LookRotation(directionPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, currWaypoint, speed * Time.deltaTime);

                yield return null;
            }
        }
        else anim.SetBool("Attack", true);
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIdx; i < path.Length; i++)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawCube(path[i], new Vector3(0.3f, 0.3f, 0.3f));
                if (i == targetIdx)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else Gizmos.DrawLine(path[i - 1], path[i]);
            }
        }
    }
}
