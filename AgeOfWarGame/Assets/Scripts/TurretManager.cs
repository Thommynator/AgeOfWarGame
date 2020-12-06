using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (turretSlots.Count >= maxSlotAmount)
        {
            return false;
        }

        GameObject newSlot = Instantiate(emptyTurretSlot);
        newSlot.transform.SetParent(turretCanvas.transform);
        newSlot.transform.localPosition = new Vector3(1.6f, -3f + turretSlots.Count, 0);
        // newSlot.transform.localPosition = new Vector3(0, 0, 0);
        turretSlots.Add(newSlot);

        return true;
    }


}
