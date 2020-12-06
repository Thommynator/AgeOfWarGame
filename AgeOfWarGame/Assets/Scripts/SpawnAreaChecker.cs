using UnityEngine;

public class SpawnAreaChecker : MonoBehaviour
{
    public SpawnArea spawnAreaToCheck;

    void OnTriggerEnter2D(Collider2D collider)
    {
        CheckPlayerSpawnArea(collider);
        CheckEnemySpawnArea(collider);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        CheckPlayerSpawnArea(collider);
        CheckEnemySpawnArea(collider);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        CheckPlayerSpawnArea(collider);
        CheckEnemySpawnArea(collider);
    }

    private void CheckPlayerSpawnArea(Collider2D collider)
    {
        if (spawnAreaToCheck == SpawnArea.PLAYER && collider.gameObject.tag == "PlayerSoldier")
        {
            GameEvents.current.PlayerSpawnAreaFree();
        }
    }

    private void CheckEnemySpawnArea(Collider2D collider)
    {
        if (spawnAreaToCheck == SpawnArea.ENEMY && collider.gameObject.tag == "EnemySoldier")
        {
            GameEvents.current.EnemySpawnAreaFree();
        }
    }


    public enum SpawnArea
    {
        PLAYER, ENEMY
    }
}
