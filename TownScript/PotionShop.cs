using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PotionShop : NPCInteraction
{

    //Singleton
    public static PotionShop instance;


    [SerializeField] public GameObject potionMenu;
    [SerializeField] private CinemachineVirtualCamera cameraInteract;
    private void Awake()
    {
        instance = this;
    }

    public void changePriorities()
    {
        cameraInteract.Priority = 30;
    }

    public void changeBackPriorities()
    {
        cameraInteract.Priority = 1;
    }
    public void showMenu()
    {
        potionMenu.SetActive(true);
    }
}
