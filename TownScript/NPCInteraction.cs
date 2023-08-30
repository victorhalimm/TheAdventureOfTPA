using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] protected GameObject container;
    

    public void showLabelInteract()
    {
        container.SetActive(true);
    }

    public void hideLabelInteract()
    {
        container.SetActive(false);
    }
}
