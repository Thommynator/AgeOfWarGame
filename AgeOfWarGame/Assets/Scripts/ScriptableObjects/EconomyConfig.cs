using UnityEngine;

[CreateAssetMenu(fileName = "Economy Config", menuName = "ScriptableObjects/EconomyConfig", order = 1)]
public class EconomyConfig : ScriptableObject {
    [Header("General")]
    public int basicMoneyIncome;
    public float specialAttackRelativeCooldown;
    public float relativeSoldierCashback;

}