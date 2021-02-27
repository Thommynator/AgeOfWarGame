using UnityEngine;
using UnityEngine.UI;

public class CooldownController : MonoBehaviour {

    private Image overlayImage;

    private float totalCooldownDuration;
    private float remainingCooldownDuration;

    void Start() {
        this.totalCooldownDuration = 0;
        this.enabled = false;
        this.overlayImage = GetComponent<Image>();
        this.overlayImage.fillAmount = 0;
    }

    public void StartCooldown(float duration) {
        this.enabled = true;
        this.totalCooldownDuration = duration;
        this.remainingCooldownDuration = duration;
    }

    void FixedUpdate() {
        if (this.enabled) {
            remainingCooldownDuration -= Time.deltaTime;
            float ratio = remainingCooldownDuration / totalCooldownDuration;
            overlayImage.fillAmount = ratio;

            if (ratio <= 0) {
                this.enabled = false;
            }
        }
    }
}
