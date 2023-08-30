using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class QuestGiver : NPCInteraction
{
    public static QuestGiver instance;

    public GameObject questMenu;
    [SerializeField] private CinemachineVirtualCamera questCam;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        questMenu.SetActive(false);
    }

    public void changePriorities()
    {
        questCam.Priority = 30;
    }

    public void changeBackPriorities()
    {
        questCam.Priority = 1;
    }


}
