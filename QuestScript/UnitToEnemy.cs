using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitToEnemy : MonoBehaviour
{

    public Transform target;
    public Animator anim;
    
    public float speed;
    int targetIdx;
    public Vector3[] path;
    Vector3 oldTargetPos;

    public float moveInterval;
    float lastMoved;

    private bool arrived = false;
    private bool keepMoving = true;


    private void Start()
    {
        lastMoved = 0;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (target.position != oldTargetPos &&  Time.time - lastMoved >= moveInterval && target )
        {
            PathRequestManager.requestPath(transform.position, target.position, OnPathFound);
            oldTargetPos = target.position;
            lastMoved = Time.time;
            keepMoving = true;
        }
        else if (arrived && Vector3.Distance(transform.position, target.position) > 3f)
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

    // sori ko/ci a*nya player to enemy ngebug aneh uda debug 4 jem ga nemu" maafkeun :(
    IEnumerator FollowPath()
    {
        if (path.Length > 0)
        {
            anim.SetBool("PathAttack", false);
            anim.SetFloat("Speed", 0.75f);
            Vector3 currWaypoint = path[0];
            while (keepMoving)
            {
                if (Vector3.Distance(transform.position, currWaypoint) <= 1f)
                {
                    targetIdx++;
                    if (targetIdx >= path.Length)
                    {
                        anim.SetFloat("Speed", 0f);
                        anim.SetBool("PathAttack", true);
                        arrived = true;
                        yield break;
                    }
                    currWaypoint = path[targetIdx];
                }

                Vector3 directionPos = (target.position - transform.position).normalized;
                directionPos.y = 0;
                if (directionPos != Vector3.zero) 
                {
                    Quaternion rotation = Quaternion.LookRotation(directionPos);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
                }
                transform.position = Vector3.MoveTowards(transform.position, currWaypoint, speed * Time.deltaTime);

                yield return null;
            }
        }
        else anim.SetBool("PathAttack", true);
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

    public void stopFollowing()
    {
        if (path != null)
        {
            StopCoroutine(FollowPath());
            anim.SetFloat("Speed", 0);
            keepMoving = false;
            path = null;
        }
    }
}
