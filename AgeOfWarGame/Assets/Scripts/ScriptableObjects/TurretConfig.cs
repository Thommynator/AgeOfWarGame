using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Turret Config", menuName = "ScriptableObjects/TurretConfig", order = 1)]
public class TurretConfig : ScriptableObject
{
    [Header("General")]
    public int buyingPrice;
    public int sellingPrice;


    [Header("Combat")]
    public float strength;
    public float attackCooldown;
    public float attackRange;
    public bool canAttackMultiple;

}