using System.Collections.Generic;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour {

    public List<SkillTreeUpgradeConfig> unlockedSkills;
    private SkillTreeUpgradeConfig aggregatedStats;
    public static SkillTreeManager current;
    private bool isInSkillTreeView;
    private GameObject mainCamera;
    private GameObject hudCanvas;
    private float gameCameraHeightY;
    private float skillTreeCameraHeightY;


    void Awake() {
        current = this;
    }

    void Start() {
        this.isInSkillTreeView = false;
        this.mainCamera = GameObject.Find("Main Camera");
        this.hudCanvas = GameObject.Find("HudCanvas");
        this.gameCameraHeightY = mainCamera.transform.position.y;
        this.skillTreeCameraHeightY = 18;
        UpdateAggregatedStats();
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

    public bool TryToUnlockSkill(SkillComponent skill) {
        if (IsUnlockable(skill)) {
            UnlockSkill(skill);
            return true;
        }
        return false;
    }

    public bool IsUnlockable(SkillComponent skill) {
        // check if player has enough money & xp
        if (XpManager.current.xp < skill.config.xpCosts || PurchaseManager.current.playerMoney < skill.config.moneyCosts) {
            return false;
        }

        // check if skill is already unlocked
        if (unlockedSkills.Contains(skill.config)) {
            return false;
        }

        // check if skill requirements are fulfilled
        foreach (SkillTreeUpgradeConfig requirement in skill.config.requirements) {
            if (!unlockedSkills.Contains(requirement)) {
                return false;
            }
        }
        return true;
    }

    private void UnlockSkill(SkillComponent skill) {
        Debug.Log("Unlocked Skill " + skill.config.skillName);
        unlockedSkills.Add(skill.config);
        GameEvents.current.DecreasecreaseXp(skill.config.xpCosts);
        GameEvents.current.DecreaseMoney(skill.config.moneyCosts);
        UpdateAggregatedStats();
    }

    private void UpdateAggregatedStats() {

        this.aggregatedStats = ScriptableObject.CreateInstance<SkillTreeUpgradeConfig>();

        this.aggregatedStats.soldierRelativePrice = 1;
        this.aggregatedStats.soldierRelativeStrength = 1;
        this.aggregatedStats.soldierRelativeRange = 1;

        this.aggregatedStats.turretRelativePrice = 1;
        this.aggregatedStats.turretRelativeStrength = 1;
        this.aggregatedStats.turretRelativeRange = 1;

        this.aggregatedStats.constantMoneyIncome = 0;
        this.aggregatedStats.specialAttackRelativeCooldown = 1;
        this.aggregatedStats.relativeSoldierCashback = 0;

        this.unlockedSkills.ForEach(skill => {

            // soldier
            aggregatedStats.soldierRelativeStrength *= percentToFactor(skill.soldierRelativeStrength);
            aggregatedStats.soldierRelativePrice *= percentToFactor(skill.soldierRelativePrice);
            aggregatedStats.soldierRelativeRange *= percentToFactor(skill.soldierRelativeRange);

            // engineer
            aggregatedStats.turretRelativeStrength *= percentToFactor(skill.turretRelativeStrength);
            aggregatedStats.turretRelativePrice *= percentToFactor(skill.turretRelativePrice);
            aggregatedStats.turretRelativeRange *= percentToFactor(skill.turretRelativeRange);
            if (skill.canBuildTurrets) {
                aggregatedStats.canBuildTurrets = true;
            }
            // economy
            aggregatedStats.constantMoneyIncome += skill.constantMoneyIncome;
            aggregatedStats.specialAttackRelativeCooldown *= percentToFactor(skill.specialAttackRelativeCooldown);
            if (Mathf.Abs(aggregatedStats.relativeSoldierCashback) < 0.01f) {
                aggregatedStats.relativeSoldierCashback += percentToFactor(skill.relativeSoldierCashback);
            } else {
                aggregatedStats.relativeSoldierCashback *= percentToFactor(skill.relativeSoldierCashback);
            }
        });

    }

    private float percentToFactor(float numberInPercent) {
        return (numberInPercent + 100) / 100;
    }

    public SoldierConfig GetSoldierConfigWithUpgrades(SoldierConfig soldierConfig) {
        SoldierConfig copy = Instantiate<SoldierConfig>(soldierConfig);
        copy.strength = Mathf.CeilToInt(soldierConfig.strength * this.aggregatedStats.soldierRelativeStrength);
        copy.price = Mathf.FloorToInt(soldierConfig.price * this.aggregatedStats.soldierRelativePrice);
        copy.rangeAttackRange = Mathf.CeilToInt(soldierConfig.rangeAttackRange * this.aggregatedStats.soldierRelativeRange);
        return copy;
    }

    public TurretConfig GetTurretConfigWithUpgrades(TurretConfig turretConfig) {
        TurretConfig copy = Instantiate<TurretConfig>(turretConfig);
        copy.strength = Mathf.CeilToInt(turretConfig.strength * this.aggregatedStats.turretRelativeStrength);
        copy.buyingPrice = Mathf.FloorToInt(turretConfig.buyingPrice * this.aggregatedStats.turretRelativePrice);
        copy.attackRange = Mathf.CeilToInt(turretConfig.attackRange * this.aggregatedStats.turretRelativeRange);
        return copy;
    }

    public bool CanBuildTurrets() {
        return this.aggregatedStats.canBuildTurrets;
    }

    public EconomyConfig GetEconomyConfigWithUpgrades() {
        EconomyConfig config = ScriptableObject.CreateInstance<EconomyConfig>();
        config.basicMoneyIncome = Mathf.CeilToInt(this.aggregatedStats.constantMoneyIncome);
        config.specialAttackRelativeCooldown = this.aggregatedStats.specialAttackRelativeCooldown;
        config.relativeSoldierCashback = this.aggregatedStats.relativeSoldierCashback - 1;
        return config;
    }

}
