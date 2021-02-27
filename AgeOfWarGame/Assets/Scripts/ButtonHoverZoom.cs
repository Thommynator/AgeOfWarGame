using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverZoom : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    private LTDescr loopAnimation;

    public void OnPointerEnter(PointerEventData eventData) {
        this.loopAnimation = LeanTween.scale(this.gameObject, Vector3.one * 1.1f, 0.3f).setLoopPingPong();
    }

    public void OnPointerExit(PointerEventData eventData) {
        LeanTween.cancel(this.loopAnimation.id);
        LeanTween.scale(this.gameObject, Vector3.one, 0.3f);
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
