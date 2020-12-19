using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurretManager : MonoBehaviour
{


    public List<TurretSlot> turretSlots;

    public GameObject emptyTurretSlotPrefab;

    private GameObject turretCanvas;

    private int maxSlotAmount;

    void Start()
    {
        this.maxSlotAmount = 3;
        this.turretSlots = new List<TurretSlot>(maxSlotAmount);
        this.turretCanvas = GameObject.Find("TurretCanvas");
    }

    public bool AddNewEmptyTurretSlot()
    {
        if (IsMaxTurrsetSlotLimitReached())
        {
            return false;
        }

        GameObject emptySlot = Instantiate(this.emptyTurretSlotPrefab);
        emptySlot.GetComponentInChildren<TurretSelector>().slotId = this.turretSlots.Count;
        emptySlot.transform.SetParent(this.turretCanvas.transform);
        emptySlot.transform.localPosition = new Vector3(1.6f, -3f + this.turretSlots.Count, 0);
        this.turretSlots.Add(new TurretSlot(emptySlot, true));
        return true;
    }

    public int GetCostsForNextTurretSlot()
    {
        List<int> costs = new List<int>(new int[3] { 250, 800, 2500 });
        return costs[this.turretSlots.Count];
    }

    public bool IsMaxTurrsetSlotLimitReached()
    {
        return turretSlots.Count >= maxSlotAmount;
    }

    public bool IsSlotFree(int slotId)
    {
        return this.turretSlots[slotId].isFree;
    }

    public int GetTurretCosts(int turretType)
    {
        return EpochManager.current.GetTurretsOfCurrentPlayerEpoch()[turretType].GetComponent<TurretBehavior>().turretConfig.buyingPrice;
    }

    public void BuyTurretForSlot(int turretType, int slotId)
    {
        List<GameObject> turretsPerEpoch = EpochManager.current.GetTurretsOfCurrentPlayerEpoch();
        GameObject turret = Instantiate(turretsPerEpoch[turretType], this.turretSlots[slotId].turret.transform);
        turret.transform.SetParent(this.turretCanvas.transform);
        turret.GetComponentInChildren<TurretSelector>().slotId = slotId;
        turret.tag = "PlayerTurret";
        Destroy(this.turretSlots[slotId].turret);
        this.turretSlots[slotId] = new TurretSlot(turret, false);
    }

    public void RemoveTurretFromTurretSlot(int slotId)
    {
        GameObject currentTurret = this.turretSlots[slotId].turret;
        GameObject emptySlot = Instantiate(this.emptyTurretSlotPrefab, currentTurret.transform);
        emptySlot.transform.SetParent(this.turretCanvas.transform);
        emptySlot.GetComponentInChildren<TurretSelector>().slotId = slotId;
        Destroy(currentTurret);
        this.turretSlots[slotId] = new TurretSlot(emptySlot, true);
    }

    public class TurretSlot
    {
        public GameObject turret;
        public bool isFree;

        public TurretSlot(GameObject turret, bool isFree)
        {
            this.turret = turret;
            this.isFree = isFree;
        }
    }


}
