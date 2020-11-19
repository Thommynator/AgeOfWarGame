using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyDisplayer : MonoBehaviour
{

    public GameObject gameManager;

    private TextMeshProUGUI moneyText;

    void Start()
    {
        moneyText = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "$ " + gameManager.GetComponent<PurchaseManager>().playerMoney;
    }
}
