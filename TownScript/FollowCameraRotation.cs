using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraRotation : MonoBehaviour
{
    public Camera cameraToLookAt;

    // Update is called once per frame 
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cameraToLookAt.transform.position);
        transform.rotation = Quaternion.Euler(90, transform.rotation.eulerAngles.y, 0);
    }
}
