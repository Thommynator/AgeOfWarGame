using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int slotId;
    private PurchaseManager purchaseManager;
    private bool isMouseOnObject;

    void Start()
    {
        this.purchaseManager = GameObject.Find("GameManager").GetComponent<PurchaseManager>();
        this.isMouseOnObject = false;
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

        // show current selection, if not already shown
        if (!this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(true);
            this.gameObject.transform.localScale = Vector3.zero;
            LeanTween.scale(this.gameObject, Vector3.one, 0.2f);
        }

        StartCoroutine(AutoHideTurretOptionsWhenMouseIsNotOn(3));
    }

    public void HideTurretOptions()
    {
        if (TooltipSystem.GetCurrentTooltip() != null && TooltipSystem.GetCurrentTooltip().GetType() == typeof(TurretTooltip))
        {
            TooltipSystem.Hide();
        }
        if (this.gameObject.activeSelf)
        {
            LeanTween.scale(this.gameObject, Vector3.zero, 0.2f).setOnComplete(() => this.gameObject.SetActive(false));
        }
    }

    private IEnumerator AutoHideTurretOptionsWhenMouseIsNotOn(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (!this.isMouseOnObject)
        {
            HideTurretOptions();
        }
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.isMouseOnObject = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.isMouseOnObject = false;
        StartCoroutine(AutoHideTurretOptionsWhenMouseIsNotOn(0.5f));
    }
}
