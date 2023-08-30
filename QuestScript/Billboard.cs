using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private MainCamera cam;

    public void Start()
    {
        cam = MainCamera.instance;
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.transform.forward);
    }
}
