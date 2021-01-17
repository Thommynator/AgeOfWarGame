using UnityEngine;

[CreateAssetMenu(fileName = "Special Attack Config", menuName = "ScriptableObjects/SpecialAttackConfig", order = 1)]
public class SpecialAttackConfig : ScriptableObject
{
    public GameObject projectile;

    public string attackName;
    public string description;
    public int amountOfProjectiles;
    public int xpCosts;
    public float attackCooldown;
    public float strength;
    public float minTimeBetweenSpawning;
    public float maxTimeBetweenSpawning;
    public float minX;
    public float maxX;
}