using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillComponent : MonoBehaviour {

    public SkillTreeUpgradeConfig config;
    private bool isUnlocked;


    // Start is called before the first frame update
    void Start() {
        this.isUnlocked = false;
    }

    // Update is called once per frame
    void Update() {
        if (isUnlocked) {
            // show unlocked visualization
            Debug.Log(this.config.skillName + " is unlocked.");
        } else if (SkillTreeManager.current.IsUnlockable(this)) {
            // show unlockable visualization
            Debug.Log(this.config.skillName + " is unlockable.");
        }
    }

    public void TryToUnlockSkill() {
        this.isUnlocked = SkillTreeManager.current.TryToUnlockSkill(this);
    }

}
