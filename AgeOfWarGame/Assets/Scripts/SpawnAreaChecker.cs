using UnityEngine;
using System;

public class SpawnAreaChecker : MonoBehaviour
{
    public SpawnArea spawnAreaToCheck;

    void OnTriggerEnter2D(Collider2D collider)
    {
        CheckPlayerSpawnArea(collider, GameEvents.current.PlayerSpawnAreaBlocked);
        CheckEnemySpawnArea(collider, GameEvents.current.EnemySpawnAreaBlocked);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        CheckPlayerSpawnArea(collider, GameEvents.current.PlayerSpawnAreaBlocked);
        CheckEnemySpawnArea(collider, GameEvents.current.EnemySpawnAreaBlocked);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        CheckPlayerSpawnArea(collider, GameEvents.current.PlayerSpawnAreaFree);
        CheckEnemySpawnArea(collider, GameEvents.current.EnemySpawnAreaFree);
    }

    private void CheckPlayerSpawnArea(Collider2D collider, Action function)
    {
        if (spawnAreaToCheck == SpawnArea.PLAYER && collider.gameObject.tag == "PlayerSoldier")
        {
            function();
        }
    }

    private void CheckEnemySpawnArea(Collider2D collider, Action function)
    {
        if (spawnAreaToCheck == SpawnArea.ENEMY && collider.gameObject.tag == "EnemySoldier")
        {
            function();
        }
    }


    public enum SpawnArea
    {
        PLAYER, ENEMY
    }
}
