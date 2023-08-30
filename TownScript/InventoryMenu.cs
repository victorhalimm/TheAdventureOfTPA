using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    [SerializeField] private GameObject itemParent;
    private InventorySlot[] slots;
    private Inventory inventory;
    public GameObject inventoryUI;

    public static InventoryMenu instance;

    
    public void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        slots = GetComponentsInChildren<InventorySlot>();
        inventory = Inventory.instance;
        inventoryUI.SetActive(false);
    }

    private void Start()
    {
        inventory.onChangedCallBack += updateUI;
    }



    public void updateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.itemList.Count)
            {
                slots[i].item = inventory.itemList[i];
                slots[i].addItemToInventory();
                slots[i].enableItem();
            }
        }
    }





}
