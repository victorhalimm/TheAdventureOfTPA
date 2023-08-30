using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //Inventory Singleton
    public static Inventory instance;

    public int space = 8;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public delegate void OnChangedInventory();
    public OnChangedInventory onChangedCallBack;


    public List<AbstractItem> itemList;
    public bool Add(AbstractItem item)
    {
        if (itemList.Count >= space)
        {
            return false;
        }
        
        itemList.Add(item);
        InventoryMenu.instance.updateUI();
        return true;
    }

    public void Remove(AbstractItem item)
    {
        itemList.Remove(item);
        if (onChangedCallBack != null) onChangedCallBack.Invoke();
    }
}
