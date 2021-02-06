using UnityEngine;

public class SkillTreeManager : MonoBehaviour {

    private bool isInSkillTreeView;
    private GameObject mainCamera;
    private GameObject hudCanvas;

    private float gameCameraHeightY;
    private float skillTreeCameraHeightY;


    void Start() {
        this.isInSkillTreeView = false;
        this.mainCamera = GameObject.Find("Main Camera");
        this.hudCanvas = GameObject.Find("HudCanvas");
        this.gameCameraHeightY = mainCamera.transform.position.y;
        this.skillTreeCameraHeightY = 18;
    }

    void Update() {

    }

    public void SwitchToSkillTreeView() {
        if (this.isInSkillTreeView) {
            return;
        }


        this.mainCamera.LeanMoveY(skillTreeCameraHeightY, 1f).setEaseInOutCirc().setOnComplete(() => Time.timeScale = 0.0f);
        ToggleUiLayerCameraVisibility();
        this.isInSkillTreeView = true;
    }

    public void SwitchToGameView() {
        if (!isInSkillTreeView) {
            return;
        }


        Time.timeScale = 1f;
        mainCamera.LeanMoveY(this.gameCameraHeightY, 1f).setEaseInOutCirc().setOnComplete(() => ToggleUiLayerCameraVisibility());
        this.isInSkillTreeView = false;
    }

    private void ToggleUiLayerCameraVisibility() {
        this.mainCamera.GetComponent<Camera>().cullingMask ^= 1 << LayerMask.NameToLayer("UI");
    }
}
