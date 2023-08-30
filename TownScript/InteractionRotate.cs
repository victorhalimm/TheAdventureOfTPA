using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionRotate : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {

        Quaternion rotation = Quaternion.LookRotation(cam.position - text.transform.position);
        text.transform.rotation = Quaternion.Euler(0f, rotation.eulerAngles.y + 180f, rotation.eulerAngles.z);
        
    }
}
