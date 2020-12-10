using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSelector : MonoBehaviour
{

    public int slotId;

    private PurchaseManager purchaseManager;

    void Start()
    {
        this.purchaseManager = GameObject.Find("GameManager").GetComponent<PurchaseManager>();
        HideTurretOptions();
    }

    public void ShowTurretOptions()
    {
        // hide all other selections first
        GameObject[] allSelections = GameObject.FindGameObjectsWithTag("TurretOptions");
        foreach (GameObject selection in allSelections)
        {
            selection.GetComponent<TurretSelector>().HideTurretOptions();
        }

        // show current selection
        this.gameObject.SetActive(true);
        StartCoroutine(AutoHideTurretOptions(5));
    }

    public void HideTurretOptions()
    {
        this.gameObject.SetActive(false);
    }

    private IEnumerator AutoHideTurretOptions(float duration)
    {
        yield return new WaitForSeconds(duration);
        HideTurretOptions();
    }

    public void TryToBuyTurret(int turretType)
    {
        this.purchaseManager.TryToBuyNewTurret(this.slotId, turretType);
    }

    public void SellTurret()
    {
        this.purchaseManager.SellExistingTurret(this.slotId);
    }
}