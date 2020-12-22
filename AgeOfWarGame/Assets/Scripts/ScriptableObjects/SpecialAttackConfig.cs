using UnityEngine;

[CreateAssetMenu(fileName = "Special Attack Config", menuName = "ScriptableObjects/SpecialAttackConfig", order = 1)]
public class SpecialAttackConfig : ScriptableObject
{
    public GameObject projectile;
    public int amountOfProjectiles;
    public int xpCosts;
    public float cooldownDuration;
    public float damage;
    public float minTimeBetweenSpawning;
    public float maxTimeBetweenSpawning;
    public float minX;
    public float maxX;
}