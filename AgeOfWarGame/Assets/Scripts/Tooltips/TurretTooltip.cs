using UnityEngine;
using TMPro;

public class TurretTooltip : Tooltip
{
    [Header("General")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI costsText;
    public TextMeshProUGUI refundText;
    public TextMeshProUGUI specialAbilityText;


    [Header("Combat")]
    public TextMeshProUGUI strengthText;
    public TextMeshProUGUI cooldownText;
    public TextMeshProUGUI rangeText;

    public override void SetContent(ScriptableObject scriptableObject)
    {
        if (scriptableObject.GetType() != typeof(TurretConfig))
        {
            return;
        }

        TurretConfig turretConfig = (TurretConfig)scriptableObject;

        this.nameText.text = turretConfig.turretName;
        this.descriptionText.text = turretConfig.description;
        this.costsText.text = turretConfig.buyingPrice.ToString() + "$";
        this.refundText.text = turretConfig.sellingPrice.ToString() + "$";
        this.specialAbilityText.text = turretConfig.specialAbility;

        this.strengthText.text = turretConfig.strength.ToString();
        this.cooldownText.text = turretConfig.attackCooldown.ToString() + "s";
        this.rangeText.text = turretConfig.attackRange.ToString();
    }
}
