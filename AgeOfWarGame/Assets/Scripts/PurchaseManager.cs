using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseManager : MonoBehaviour
{
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

        List<GameObject> soldiersOfCurrentEpoch = EpochManager.current.GetSoldiersOfCurrentPlayerEpoch();

        if (soldierType < 0 || soldierType > soldiersOfCurrentEpoch.Count - 1)
        {
            return;
        }

        GameObject soldier = soldiersOfCurrentEpoch[soldierType];
        SoldierConfig soldierConfig = soldier.GetComponent<SoldierBehavior>().soldierConfig;
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
