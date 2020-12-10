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
        HideTurretSelection();
    }

    public void ShowTurretSelection()
    {
        // hide all other selections first
        GameObject[] allSelections = GameObject.FindGameObjectsWithTag("TurretSelection");
        foreach (GameObject selection in allSelections)
        {
            selection.GetComponent<TurretSelector>().HideTurretSelection();
        }

        // show current selection
        this.gameObject.SetActive(true);
        StartCoroutine(AutoHideTurretSelection(5));
    }

    public void HideTurretSelection()
    {
        this.gameObject.SetActive(false);
    }

    private IEnumerator AutoHideTurretSelection(float duration)
    {
        yield return new WaitForSeconds(duration);
        HideTurretSelection();
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