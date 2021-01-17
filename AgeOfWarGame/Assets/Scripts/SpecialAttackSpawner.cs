using System.Collections;
using UnityEngine;

public class SpecialAttackSpawner : MonoBehaviour
{
    public GameObject cooldownVisualization;

    private XpManager xpManager;

    private float timeOfNextPossibleExecution;

    public void Start()
    {
        this.xpManager = GameObject.Find("GameManager").GetComponent<XpManager>();
        this.timeOfNextPossibleExecution = 0;
    }

    public void StartAttackForPlayer()
    {
        SpecialAttackConfig config = EpochManager.current.GetSpecialAttackConfigOfCurrentPlayerEpoch();

        if (Time.time > timeOfNextPossibleExecution)
        {
            if (this.xpManager.xp >= config.xpCosts)
            {
                this.timeOfNextPossibleExecution = Time.time + config.attackCooldown;
                GameEvents.current.DecreasecreaseXp(config.xpCosts);
                this.cooldownVisualization.GetComponent<CooldownController>().StartCooldown(config.attackCooldown);
                StartCoroutine(SpawnRandomly(true));
            }
            else
            {
                Debug.Log("Not enough XP!");
            }
        }
        else
        {
            Debug.Log("Special attack is on cooldown!");
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
