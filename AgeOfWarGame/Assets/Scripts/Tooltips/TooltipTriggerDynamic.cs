using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTriggerDynamic : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private static LTDescr delay;
    public Category category;
    public int index;

    public void OnPointerEnter(PointerEventData eventData) {
        ScriptableObject scriptableObject;
        if (category.Equals(Category.SOLDIERS)) {
            scriptableObject = EpochManager.current.GetSoldiersOfCurrentPlayerEpoch()[index].GetComponent<SoldierBehavior>().soldierConfig;
        } else if (category.Equals(Category.TURRETS)) {
            scriptableObject = EpochManager.current.GetTurretsOfCurrentPlayerEpoch()[index].GetComponent<TurretBehavior>().turretConfig;
        } else if (category.Equals(Category.SPECIAL_ATTACK)) {
            scriptableObject = EpochManager.current.GetSpecialAttackConfigOfCurrentPlayerEpoch();
        } else {
            return;
        }

        delay = LeanTween.delayedCall(0.5f, () => {
            TooltipSystem.Show(scriptableObject);
        }).setIgnoreTimeScale(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        LeanTween.cancel(delay.uniqueId);
        TooltipSystem.Hide();
    }

    public enum Category {
        SOLDIERS, TURRETS, SPECIAL_ATTACK
    }

}
