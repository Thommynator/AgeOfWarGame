using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseManager : MonoBehaviour
{

    public List<SoldierConfig> soldiersPerEpoch;

    public int playerMoney;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TryToBuySoldier(int soldierType)
    {
        if (soldierType < 0 || soldierType > soldiersPerEpoch.Count - 1)
        {
            return;
        }

        SoldierConfig soldierConfig = soldiersPerEpoch[soldierType].GetComponent<SoldierConfig>();
        if (playerMoney >= soldierConfig.price)
        {
            Debug.Log("Buy soldier " + soldierType);
            playerMoney -= soldierConfig.price;

        }
        else
        {
            Debug.Log("Not enough money!");

        }
    }



}
