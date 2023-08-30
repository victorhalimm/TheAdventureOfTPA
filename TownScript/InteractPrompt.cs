using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPrompt : MonoBehaviour
{
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private GameObject container;

    private void Update()
    {
        if (playerInteract.getNpc() != null)
        {
            NPCInteraction npcNear = playerInteract.getNpc();
            if (playerInteract.isInteracting) Hide();
            else npcNear.showLabelInteract();
        }
        else Hide();
      
    }

    public void Hide()
    {
        container.SetActive(false);
    }
}
