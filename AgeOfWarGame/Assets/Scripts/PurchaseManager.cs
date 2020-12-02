using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseManager : MonoBehaviour
{

    public List<GameObject> soldiersPerEpoch;

    public int playerMoney;

    public GameObject queue;

    void Start()
    {
        GameEvents.current.onIncreaseMoney += (int money) => this.playerMoney += money;
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

        GameObject soldier = soldiersPerEpoch[soldierType];
        SoldierConfig soldierConfig = soldier.GetComponentInChildren<SoldierConfig>();
        if (playerMoney >= soldierConfig.price)
        {
            if (queue.GetComponent<Queue>().AddSoldierToQueue(soldier))
            {
                playerMoney -= soldierConfig.price;
            }
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

}
