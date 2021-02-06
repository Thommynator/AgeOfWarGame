using UnityEngine;


[CreateAssetMenu(fileName = "Skill Tree Upgrade", menuName = "ScriptableObjects/SkillTreeUpgrade", order = 1)]
public class SkillTreeUpgradeConfig : ScriptableObject
{

    [Header("General")]
    public string skillName;
    public string description;
    public float moneyCosts;
    public float xpCosts;

    [Header("Soldier")]
    public float soldierRelativeStrength;
    public float soldierRelativePrice;
    public float soldierRelativeRange;

    [Header("Turret")]
    public float turretRelativeStrength;
    public float turretRelativePrice;
    public float turretRelativeRange;

    [Header("Economy")]
    public float constantMoneyIncome;
    public float specialAttackRelativeCooldown;
    public float relativeSoldierCashback;


}
