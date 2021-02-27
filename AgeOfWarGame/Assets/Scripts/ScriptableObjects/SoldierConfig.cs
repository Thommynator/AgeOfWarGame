using UnityEngine;

[CreateAssetMenu(fileName = "Soldier Config", menuName = "ScriptableObjects/SoldierConfig", order = 1)]
public class SoldierConfig : ScriptableObject {
    [Header("General")]
    public string soldierName;
    public string description;
    public int price;
    public float health;
    public float maxSpeed;
    public float spawnDuration;
    public int rewardMoney;
    public int rewardXp;

    [Header("Combat")]
    public float strength;
    public float attackCooldown;
    public AttackType attackType;
    public float meleeAttackRange;
    public float rangeAttackRange;
    public bool canAttackMultiple;
    public GameObject rangeProjectile;
    public bool isMoralActive;


    [Header("Visuals")]
    public Sprite characterQueueIcon;


    public enum AttackType {
        MELEE, RANGE
    }
}