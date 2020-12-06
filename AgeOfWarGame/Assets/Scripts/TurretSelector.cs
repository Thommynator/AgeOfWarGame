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
        HidePurchasableTurretSelection();

    }

    public void ShowPurchasableTurretSelection()
    {
        GameObject[] allSelections = GameObject.FindGameObjectsWithTag("TurretSelection");
        foreach (GameObject selection in allSelections)
        {
            selection.GetComponent<TurretSelector>().HidePurchasableTurretSelection();
        }
        this.gameObject.SetActive(true);
        StartCoroutine(AutoHidePurchasableTurretOptions(5));
    }

    public void HidePurchasableTurretSelection()
    {
        this.gameObject.SetActive(false);
    }

    private IEnumerator AutoHidePurchasableTurretOptions(float duration)
    {
        yield return new WaitForSeconds(duration);
        HidePurchasableTurretSelection();
    }

    public void TryToBuyTurret(int turretType)
    {
        this.purchaseManager.TryToBuyNewTurret(this.slotId, turretType);
    }
}