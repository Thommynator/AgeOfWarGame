using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverZoom : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    private LTDescr loopAnimation = new LTDescr();

    public void OnPointerEnter(PointerEventData eventData) {
        if (GetComponent<Button>().IsInteractable()) {
            this.loopAnimation = LeanTween.scale(this.gameObject, Vector3.one * 1.1f, 0.3f).setIgnoreTimeScale(true).setLoopPingPong();
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        LeanTween.cancel(this.loopAnimation.id);
        LeanTween.scale(this.gameObject, Vector3.one, 0.3f).setIgnoreTimeScale(true);
    }

}
