using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform shopParent;
    [SerializeField] private List<ConsumableItem> consumables;

    private ShopSlot[] slots;
    void Start()
    {
        slots = shopParent.GetComponentsInChildren<ShopSlot>();

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < consumables.Count)
            {
                slots[i].item = consumables[i];
                slots[i].addItemToShop();
                slots[i].enableItem();
            }
        }
    }

}
