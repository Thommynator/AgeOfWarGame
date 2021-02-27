using UnityEngine;
using UnityEngine.UI;

public class SkillComponent : MonoBehaviour {

    public SkillTreeUpgradeConfig config;
    private bool isUnlocked;


    // Start is called before the first frame update
    void Start() {
        this.isUnlocked = false;
    }

    // Update is called once per frame
    void Update() {
        if (this.IsUnlockable()) {
            GetComponent<Button>().interactable = true;
            GetComponent<Image>().sprite = config.skillUnlockableSprite;
        } else if (!this.IsUnlocked()) {
            GetComponent<Button>().interactable = false;
        } else if (this.IsUnlocked()) {
            GetComponent<Image>().sprite = config.skillUnlockedSprite;
        }
    }

    public void TryToUnlockSkill() {
        if (!this.IsUnlocked()) {
            this.isUnlocked = SkillTreeManager.current.TryToUnlockSkill(this);
        }
    }

    public bool IsUnlocked() {
        return this.isUnlocked;
    }

    public bool IsUnlockable() {
        return SkillTreeManager.current.IsUnlockable(this);
    }





}
