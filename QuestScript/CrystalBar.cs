using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalBar : MonoBehaviour
{

    public Slider slider;
    public Image fill;

    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
    }
    public void setHealth(int health)
    {
        slider.value = health;
    }
}
