using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractItem : ScriptableObject
{
    public string itemName = "";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public int quantity = 0;
    public int price;


}
