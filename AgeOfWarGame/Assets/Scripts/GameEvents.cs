using System;
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

    public event Action<int> onIncreaseXp;

    public void IncreaseXp(int xp)
    {
        if (onIncreaseXp != null)
        {
            onIncreaseXp(xp);
        }
    }

    public event Action<int> onDecreaseXp;

    public void DecreasecreaseXp(int xp)
    {
        if (onDecreaseXp != null)
        {
            onDecreaseXp(xp);
        }
    }

    public event Action onGameOver;

    public void GameOver()
    {
        if (onGameOver != null)
        {
            onGameOver();
        }
    }


}
