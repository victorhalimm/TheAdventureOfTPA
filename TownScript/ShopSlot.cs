using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{

    [SerializeField] private GameObject imageObject;
    [SerializeField] private GameObject priceField;
    [SerializeField] private GameObject nameField;

    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI nameText;

    public AbstractItem item;


    public void addItemToShop()
    {
        setImage(item.icon);
        setPrice(item.price);
        setName(item.itemName);
    }

    public void purchaseItem()
    {
        Debug.Log(item);
        if (Inventory.instance.Add(item) == true)
        {
            Debug.Log("Item Succesfully Purchased and added to Inventory");
            PlayerData.instance.purchaseItem(item.price);
            PlayerData.instance.onPurchaseCallBack.Invoke();
        }
        else Debug.Log("Maximum capacity has been reached");
    }

    public void setImage(Sprite image)
    {
        itemImage.sprite = image;
    }

    public void setPrice(int price)
    {
        string priceToText = price.ToString();
        priceText.text = priceToText;
    }

    public void setName(string name)
    {
        nameText.text = name;
    }

    public void enableItem()
    {
        imageObject.SetActive(true);
        priceField.SetActive(true);
        nameField.SetActive(true);
    }
}
