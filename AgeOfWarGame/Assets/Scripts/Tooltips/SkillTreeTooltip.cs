using UnityEngine;
using TMPro;

public class SkillTreeTooltip : Tooltip {
    [Header("General")]
    public TextMeshProUGUI skillNameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI moneyCostsText;
    public TextMeshProUGUI xpCostsText;

    [Header("Soldier")]
    public TextMeshProUGUI soldierRelativeStrengthText;
    public TextMeshProUGUI soldierRelativePriceText;
    public TextMeshProUGUI soldierRelativeRangeText;

    [Header("Turret")]
    public TextMeshProUGUI turretRelativeStrengthText;
    public TextMeshProUGUI turretRelativePriceText;
    public TextMeshProUGUI turretRelativeRangeText;

    [Header("Economy")]
    public TextMeshProUGUI constantMoneyIncomeText;
    public TextMeshProUGUI specialAttackRelativeCooldownText;
    public TextMeshProUGUI relativeSoldierCashbackText;

    public override void SetContent(ScriptableObject scriptableObject) {
        if (scriptableObject.GetType() != typeof(SkillTreeUpgradeConfig)) {
            return;
        }

        SkillTreeUpgradeConfig skillTreeUpgrade = (SkillTreeUpgradeConfig)scriptableObject;

        this.skillNameText.text = skillTreeUpgrade.skillName;
        this.descriptionText.text = skillTreeUpgrade.description;
        this.moneyCostsText.text = skillTreeUpgrade.moneyCosts.ToString() + " $";
        this.xpCostsText.text = skillTreeUpgrade.xpCosts.ToString() + " XP";

        // Soldier
        float relativeStrength = skillTreeUpgrade.soldierRelativeStrength + skillTreeUpgrade.moralPerSoldierRelativeStrength;
        AdjustTextStyle(this.soldierRelativeStrengthText, relativeStrength);
        this.soldierRelativeStrengthText.text = AddSignToAttribute(relativeStrength) + "%";

        AdjustTextStyle(this.soldierRelativePriceText, skillTreeUpgrade.soldierRelativePrice);
        this.soldierRelativePriceText.text = AddSignToAttribute(skillTreeUpgrade.soldierRelativePrice) + "%";

        AdjustTextStyle(this.soldierRelativeRangeText, skillTreeUpgrade.soldierRelativeRange);
        this.soldierRelativeRangeText.text = AddSignToAttribute(skillTreeUpgrade.soldierRelativeRange) + "%";


        // Turret
        AdjustTextStyle(this.turretRelativeStrengthText, skillTreeUpgrade.turretRelativeStrength);
        this.turretRelativeStrengthText.text = AddSignToAttribute(skillTreeUpgrade.turretRelativeStrength) + "%";

        AdjustTextStyle(this.turretRelativePriceText, skillTreeUpgrade.turretRelativePrice);
        this.turretRelativePriceText.text = AddSignToAttribute(skillTreeUpgrade.turretRelativePrice) + "%";

        AdjustTextStyle(this.turretRelativeRangeText, skillTreeUpgrade.turretRelativeRange);
        this.turretRelativeRangeText.text = AddSignToAttribute(skillTreeUpgrade.turretRelativeRange) + "%";


        // Economy
        AdjustTextStyle(this.constantMoneyIncomeText, skillTreeUpgrade.constantMoneyIncome);
        this.constantMoneyIncomeText.text = AddSignToAttribute(skillTreeUpgrade.constantMoneyIncome) + "$/sec";

        AdjustTextStyle(this.specialAttackRelativeCooldownText, skillTreeUpgrade.specialAttackRelativeCooldown);
        this.specialAttackRelativeCooldownText.text = AddSignToAttribute(skillTreeUpgrade.specialAttackRelativeCooldown) + "%";

        AdjustTextStyle(this.relativeSoldierCashbackText, skillTreeUpgrade.relativeSoldierCashback);
        this.relativeSoldierCashbackText.text = AddSignToAttribute(skillTreeUpgrade.relativeSoldierCashback) + "%";
    }

    private string AddSignToAttribute(float number) {
        // adding a minus is not needed, because this is normal behavior for negative numbers
        return number > 0 ? "+" + number.ToString() : number.ToString();
    }

    private void AdjustTextStyle(TextMeshProUGUI textObject, float value) {
        if (value == 0) {
            textObject.fontStyle = FontStyles.Normal;
            textObject.faceColor = new Color32(227, 242, 241, 75);
        } else if (value > 0) {
            textObject.fontStyle = FontStyles.Bold;
            textObject.faceColor = new Color32(227, 242, 241, 255);
        } else if (value < 0) {
            textObject.fontStyle = FontStyles.Bold;
            textObject.faceColor = new Color32(227, 242, 241, 255);
        }
    }
}
