using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    public static PlayerData instance;
    public Inventory inventory;
    public int money;

    public delegate void OnPurchaseItem();
    public OnPurchaseItem onPurchaseCallBack;
    private void Awake()
    {
        instance = this;
        inventory = Inventory.instance;
        money = 99999;
    }

    public void purchaseItem(int price)
    {
        money -= price;
        onPurchaseCallBack.Invoke();
    }





}
