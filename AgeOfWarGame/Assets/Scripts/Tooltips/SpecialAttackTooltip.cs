using UnityEngine;
using TMPro;

public class SpecialAttackTooltip : Tooltip {
    [Header("General")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI costsText;


    [Header("Combat")]
    public TextMeshProUGUI strengthText;
    public TextMeshProUGUI cooldownText;

    public override void SetContent(ScriptableObject scriptableObject) {
        if (scriptableObject.GetType() != typeof(SpecialAttackConfig)) {
            return;
        }

        SpecialAttackConfig speccialAttackConfig = (SpecialAttackConfig)scriptableObject;
        EconomyConfig economyConfig = SkillTreeManager.current.GetEconomyConfigWithUpgrades();

        this.nameText.text = speccialAttackConfig.attackName;
        this.descriptionText.text = speccialAttackConfig.description;
        this.costsText.text = speccialAttackConfig.xpCosts.ToString() + " XP";

        this.strengthText.text = speccialAttackConfig.strength.ToString();
        this.cooldownText.text = (speccialAttackConfig.attackCooldown * economyConfig.specialAttackRelativeCooldown).ToString() + " s";
    }
}
