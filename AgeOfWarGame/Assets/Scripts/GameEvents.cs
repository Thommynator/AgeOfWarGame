using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    private void Awake()
    {
        current = this;
    }


    public event Action onPlayerSpawnAreaFree;
    public void PlayerSpawnAreaFree()
    {
        if (onPlayerSpawnAreaFree != null)
        {
            onPlayerSpawnAreaFree();
        }
    }

    public event Action onPlayerSpawnAreaBlocked;
    public void PlayerSpawnAreaBlocked()
    {
        if (onPlayerSpawnAreaBlocked != null)
        {
            onPlayerSpawnAreaBlocked();
        }
    }

    public event Action onEnemySpawnAreaFree;
    public void EnemySpawnAreaFree()
    {
        if (onEnemySpawnAreaFree != null)
        {
            onEnemySpawnAreaFree();
        }
    }

    public event Action onEnemySpawnAreaBlocked;
    public void EnemySpawnAreaBlocked()
    {
        if (onEnemySpawnAreaBlocked != null)
        {
            onEnemySpawnAreaBlocked();
        }
    }

    public event Action<int> onIncreaseMoney;

    public void IncreaseMoney(int money)
    {
        if (onIncreaseMoney != null)
        {
            onIncreaseMoney(money);
        }
    }

}
