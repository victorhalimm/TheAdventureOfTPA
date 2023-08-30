
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    [SerializeField] private ThirdPlayerMovement playerMovement;
    [SerializeField] private GameObject inventoryMenu;

    private GameObject activeMenu;
    private float interactRange = 2f;
    public bool isInteracting = false;


    // Update is called once per frame

    void Update()
    {
        checkInteracting();
        checkToggleInventory();
        if (Input.GetKeyDown(KeyCode.F) && !isInteracting)
        {
            Collider[] collidesWith = Physics.OverlapSphere(transform.position, interactRange);
            Collider closestCollider = null;
            NPCInteraction closestNpcInteract = null;
            foreach (Collider collide in collidesWith)
            {
                if (collide.TryGetComponent(out NPCInteraction interaction))
                {
                    if (closestCollider == null)
                    {
                        closestCollider = collide;
                        closestNpcInteract = interaction;

                    }
                    // Search for the closest collider
                    else if (Vector3.Distance(transform.position, closestCollider.transform.position) > Vector3.Distance(transform.position, collide.transform.position))
                    {
                        closestCollider = collide;
                        closestNpcInteract = interaction;
                    }

                }
            }
            // Player is interacting with npc
            if (closestNpcInteract != null)
            {
                if (closestNpcInteract is PotionShop)
                {
                    activeMenu = PotionShop.instance.potionMenu;
                    PotionShop.instance.changePriorities();
                    PlayerDataUI.instance.showData();
                }
                else if (closestNpcInteract is QuestGiver)
                {
                    activeMenu = QuestGiver.instance.questMenu;
                    QuestGiver.instance.changePriorities();
                }
                showMenu();
                unlockCursor();
            }
        }
        // Player toggle close 
        else if (Input.GetKeyDown(KeyCode.F) && isInteracting)
        {
            PotionShop.instance.changeBackPriorities();
            QuestGiver.instance.changeBackPriorities();
            PlayerDataUI.instance.hideData();
            hideMenu();
            lockCursor();
        }
    }

    void checkToggleInventory()
    {
        //Open UI
        if (Input.GetKeyDown(KeyCode.I) && !isInteracting)
        {
            activeMenu = inventoryMenu;
            showMenu();
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            hideMenu();
        }
    }

    void unlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void lockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void checkInteracting()
    {
        if (isInteracting) playerMovement.enabled = false;
        else playerMovement.enabled = true;
    }

    public void showMenu()
    {
        activeMenu.SetActive(true);
        isInteracting = true;
    }

    public void hideMenu()
    {
        activeMenu.SetActive(false);
        activeMenu = null;
        isInteracting = false;
    }


    public NPCInteraction getNpc()
    {
        Collider[] collidesWith = Physics.OverlapSphere(transform.position, interactRange);
        Collider closestCollider = null;
        NPCInteraction closestNpcInteract = null;
        foreach (Collider collide in collidesWith)
        {
            if (collide.TryGetComponent(out NPCInteraction interaction))
            {
                if (closestCollider == null)
                {
                    closestCollider = collide;
                    closestNpcInteract = interaction;

                }
                // Search for the closest collider
                else if (Vector3.Distance(transform.position, closestCollider.transform.position) > Vector3.Distance(transform.position, collide.transform.position))
                {
                    closestCollider = collide;
                    closestNpcInteract = interaction;
                }
                
            }
        }
        return closestNpcInteract;
    }
}
