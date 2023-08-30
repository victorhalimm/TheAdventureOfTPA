using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerDataUI : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerDataUI instance;
    public GameObject dataObject;
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PlayerData.instance.onPurchaseCallBack += UpdateUI;
        moneyText.text = PlayerData.instance.money.ToString();
        hideData();
    }

    public void showData()
    {
        dataObject.SetActive(true);
    }

    public void hideData()
    {
        dataObject.SetActive(false);
    }

    void UpdateUI()
    {
        moneyText.text = PlayerData.instance.money.ToString();
    }
}
