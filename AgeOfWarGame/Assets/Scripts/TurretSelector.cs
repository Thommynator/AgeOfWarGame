using System.Collections;
using UnityEngine;

public class TurretSelector : MonoBehaviour
{
    public int slotId;
    private PurchaseManager purchaseManager;

    void Start()
    {
        this.purchaseManager = GameObject.Find("GameManager").GetComponent<PurchaseManager>();
        ShowTurretOptions();
    }

    public void ShowTurretOptions()
    {
        // hide all other selections first
        GameObject[] allSelections = GameObject.FindGameObjectsWithTag("TurretOptions");
        foreach (GameObject selection in allSelections)
        {
            if (selection.gameObject == this.gameObject)
            {
                continue;
            }
            selection.GetComponent<TurretSelector>().HideTurretOptions();
        }

        // show current selection
        this.gameObject.transform.localScale = Vector3.zero;
        this.gameObject.SetActive(true);
        LeanTween.scale(this.gameObject, Vector3.one, 0.2f);
        StartCoroutine(AutoHideTurretOptions(2));
    }

    public void HideTurretOptions()
    {
        LeanTween.scale(this.gameObject, Vector3.zero, 0.2f).setOnComplete(() => this.gameObject.SetActive(false));
    }

    private IEnumerator AutoHideTurretOptions(float duration)
    {
        yield return new WaitForSeconds(duration);
        HideTurretOptions();
    }

    public void TryToBuyTurret(int turretType)
    {
        this.purchaseManager.TryToBuyNewTurret(this.slotId, turretType);
        HideTurretOptions();
    }

    public void SellTurret()
    {
        this.purchaseManager.SellExistingTurret(this.slotId);
    }
}