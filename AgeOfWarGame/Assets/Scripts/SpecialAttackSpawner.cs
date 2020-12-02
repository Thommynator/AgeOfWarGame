using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackSpawner : MonoBehaviour
{

    private XpManager xpManager;
    public SpecialAttackConfig specialAttackConfig;

    public void Start()
    {
        this.xpManager = GameObject.Find("GameManager").GetComponent<XpManager>();
    }
    public void StartAttack()
    {
        // TODO check cooldown

        if (this.xpManager.xp >= this.specialAttackConfig.xpCosts)
        {
            GameEvents.current.DecreasecreaseXp(this.specialAttackConfig.xpCosts);
            StartCoroutine(SpawnRandomly());
        }
        else
        {
            Debug.Log("Not enough XP!");
        }
    }

    private IEnumerator SpawnRandomly()
    {
        for (int i = 0; i < this.specialAttackConfig.amountOfProjectiles; i++)
        {
            GameObject.Instantiate(this.specialAttackConfig.projectile, new Vector3(Random.Range(this.specialAttackConfig.minX, this.specialAttackConfig.maxX), 15, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(this.specialAttackConfig.minTimeBetweenSpawning, this.specialAttackConfig.maxTimeBetweenSpawning));
        }
    }
}
