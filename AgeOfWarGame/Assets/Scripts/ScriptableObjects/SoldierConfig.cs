﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Soldier Config", menuName = "ScriptableObjects/SoldierConfig", order = 1)]
public class SoldierConfig : ScriptableObject
{
    [Header("General")]
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


    [Header("Visuals")]
    public Sprite characterQueueIcon;


    public enum AttackType
    {
        MELEE, RANGE, BOTH
    }
}