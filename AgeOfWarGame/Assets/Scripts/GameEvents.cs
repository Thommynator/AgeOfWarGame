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


    public event Action onSpawnAreaFree;
    public void SpawnAreaFree()
    {
        if (onSpawnAreaFree != null)
        {
            onSpawnAreaFree();
        }
    }

    public event Action onSpawnAreaBlocked;
    public void SpawnAreaBlocked()
    {
        if (onSpawnAreaBlocked != null)
        {
            onSpawnAreaBlocked();
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
