using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackSpawner : MonoBehaviour
{
    private XpManager xpManager;

    private float timeOfPreviousExecution;

    public void Start()
    {
        this.xpManager = GameObject.Find("GameManager").GetComponent<XpManager>();
        this.timeOfPreviousExecution = 0;
    }

    public void StartAttackForPlayer()
    {
        SpecialAttackConfig config = EpochManager.current.GetSpecialAttackConfigOfCurrentPlayerEpoch();

        float deltaTime = Time.time - this.timeOfPreviousExecution;
        Debug.Log(deltaTime);
        if (deltaTime > config.cooldownDuration)
        {
            if (this.xpManager.xp >= config.xpCosts)
            {
                this.timeOfPreviousExecution = Time.time;
                GameEvents.current.DecreasecreaseXp(config.xpCosts);
                StartCoroutine(SpawnRandomly(true));
            }
            else
            {
                Debug.Log("Not enough XP!");
            }
        }
        else
        {
            Debug.Log("Cooldown!");
        }
    }

    public void StartAttackForEnemy()
    {
        StartCoroutine(SpawnRandomly(false));
    }

    private IEnumerator SpawnRandomly(bool attackEnemyTeam)
    {
        SpecialAttackConfig config = EpochManager.current.GetSpecialAttackConfigOfCurrentPlayerEpoch();

        for (int i = 0; i < config.amountOfProjectiles; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(config.minX, config.maxX), 15, 0);
            GameObject projectile = GameObject.Instantiate(config.projectile, randomPosition, Quaternion.identity);
            projectile.tag = attackEnemyTeam ? "PlayerSoldier" : "EnemySoldier";
            yield return new WaitForSeconds(Random.Range(config.minTimeBetweenSpawning, config.maxTimeBetweenSpawning));
        }
    }
}
