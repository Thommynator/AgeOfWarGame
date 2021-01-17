using UnityEngine;
using TMPro;

public class SoldierTooltip : Tooltip
{
    [Header("General")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI costsText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI spawnTimeText;

    [Header("Combat")]
    public TextMeshProUGUI strengthText;
    public TextMeshProUGUI cooldownText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI attackTypeText;

    public override void SetContent(ScriptableObject scriptableObject)
    {
        if (scriptableObject.GetType() != typeof(SoldierConfig))
        {
            return;
        }

        SoldierConfig soldierConfig = (SoldierConfig)scriptableObject;

        this.nameText.text = soldierConfig.soldierName;
        this.descriptionText.text = soldierConfig.description;
        this.costsText.text = soldierConfig.price.ToString() + " $";
        this.healthText.text = soldierConfig.health.ToString();
        this.speedText.text = soldierConfig.maxSpeed.ToString();
        this.spawnTimeText.text = soldierConfig.spawnDuration.ToString();
        this.strengthText.text = soldierConfig.strength.ToString();
        this.cooldownText.text = soldierConfig.attackCooldown.ToString() + " s";
        this.rangeText.text = soldierConfig.attackType == SoldierConfig.AttackType.MELEE ? soldierConfig.meleeAttackRange.ToString() : soldierConfig.rangeAttackRange.ToString();
        this.attackTypeText.text = soldierConfig.attackType == SoldierConfig.AttackType.MELEE ? "melee" : "range";
    }
}
