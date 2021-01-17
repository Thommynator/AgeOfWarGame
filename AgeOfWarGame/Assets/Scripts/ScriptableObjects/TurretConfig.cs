using UnityEngine;

[CreateAssetMenu(fileName = "Turret Config", menuName = "ScriptableObjects/TurretConfig", order = 1)]
public class TurretConfig : ScriptableObject
{
    [Header("General")]
    public string turretName;
    public string description;
    public int buyingPrice;
    public int sellingPrice;
    public string specialAbility;


    [Header("Combat")]
    public float strength;
    public float attackCooldown;
    public float attackRange;
    public bool canAttackMultiple;
    public GameObject projectile;
}