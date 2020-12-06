using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurretManager : MonoBehaviour
{

    List<GameObject> turretSlots;

    public GameObject emptyTurretSlot;

    private GameObject turretCanvas;

    private int maxSlotAmount;

    void Start()
    {
        this.maxSlotAmount = 3;
        this.turretSlots = new List<GameObject>(maxSlotAmount);
        this.turretCanvas = GameObject.Find("TurretCanvas");
    }

    public bool AddNewTurretSlot()
    {
        if (IsMaxTurrsetSlotLimitReached())
        {
            return false;
        }

        GameObject newSlot = Instantiate(this.emptyTurretSlot);
        newSlot.GetComponentInChildren<TurretSelector>().slotId = this.turretSlots.Count;
        newSlot.transform.SetParent(this.turretCanvas.transform);
        newSlot.transform.localPosition = new Vector3(1.6f, -3f + this.turretSlots.Count, 0);
        this.turretSlots.Add(newSlot);

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
        return true;
    }

    public int GetTurretCosts(int turretType)
    {
        List<int> costs = new List<int>(new int[3] { 500, 800, 1000 });
        return costs[turretType];
    }

    public void BuyTurretForSlot(int turretType, int slotId)
    {
        Debug.Log("Bought turret " + turretType + " for slot " + slotId);
    }


}
