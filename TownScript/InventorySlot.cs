using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private GameObject imageObject;
    [SerializeField] private GameObject nameField;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image itemImage;

    public AbstractItem item;


    public void addItemToInventory()
    {
        setImage(item.icon);
        setName(item.itemName);
    }

    public void setImage(Sprite image)
    {
        itemImage.sprite = image;
    }

    public void setName(string name)
    {
        nameText.text = name;
    }

    public void enableItem()
    {
        imageObject.SetActive(true);
        nameField.SetActive(true);
    }

}
