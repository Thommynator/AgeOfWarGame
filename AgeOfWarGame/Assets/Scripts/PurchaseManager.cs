using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseManager : MonoBehaviour
{
    public int playerMoney;

    public GameObject queue;

    private TurretManager turretManager;

    void Start()
    {
        GameEvents.current.onIncreaseMoney += (int money) => this.playerMoney += money;
        this.turretManager = GetComponent<TurretManager>();
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
        if (this.playerMoney >= soldierConfig.price)
        {
            if (queue.GetComponent<Queue>().AddSoldierToQueue(soldier))
            {
                this.playerMoney -= soldierConfig.price;
            }
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    public void TryToBuyTurretSlot()
    {

        if (!this.turretManager.IsMaxTurrsetSlotLimitReached())
        {
            int turretSlotCosts = this.turretManager.GetCostsForNextTurretSlot();
            if (this.playerMoney >= turretSlotCosts && this.turretManager.AddNewEmptyTurretSlot())
            {
                this.playerMoney -= turretSlotCosts;
            }
            else
            {
                Debug.Log("Not enough money!");
            }
        }
        else
        {
            Debug.Log("Turret slot limit is reached!");
        }

    }

    public bool TryToBuyNewTurret(int slotId, int turretType)
    {
        if (this.turretManager.IsSlotFree(slotId) && this.playerMoney >= this.turretManager.GetTurretCosts(turretType))
        {
            this.turretManager.BuyTurretForSlot(turretType, slotId);
            this.playerMoney -= this.turretManager.GetTurretCosts(turretType);
            return true;
        }
        return false;
    }

    public bool SellExistingTurret(int slotId)
    {
        int sellingPrice = this.turretManager.turretSlots[slotId].turret.GetComponent<TurretBehavior>().turretConfig.sellingPrice;
        this.turretManager.RemoveTurretFromTurretSlot(slotId);
        this.playerMoney += sellingPrice;
        return true;
    }

}
