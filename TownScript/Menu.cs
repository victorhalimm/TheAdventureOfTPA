using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Menu : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera menuCam;
    [SerializeField] private GameObject settingsUI;

    
    public void playGame()
    {
        // ganti ke kamera game
        menuCam.Priority = 1;

        LockCursor();
        gameObject.SetActive(false);
    }

    public void openSetting()
    {
        settingsUI.SetActive(true);
        gameObject.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
    }


    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
