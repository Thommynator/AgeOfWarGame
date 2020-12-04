using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackSpawner : MonoBehaviour
{

    private XpManager xpManager;
    public List<SpecialAttackConfig> specialAttackConfigs;
    public int epochIndex;

    public void Start()
    {
        this.xpManager = GameObject.Find("GameManager").GetComponent<XpManager>();
        this.epochIndex = 0;
    }

    public void StartAttack()
    {
        // TODO check cooldown

        if (this.xpManager.xp >= this.specialAttackConfigs[epochIndex].xpCosts)
        {
            GameEvents.current.DecreasecreaseXp(this.specialAttackConfigs[epochIndex].xpCosts);
            StartCoroutine(SpawnRandomly());
        }
        else
        {
            Debug.Log("Not enough XP!");
        }
    }

    private IEnumerator SpawnRandomly()
    {
        for (int i = 0; i < this.specialAttackConfigs[epochIndex].amountOfProjectiles; i++)
        {
            GameObject.Instantiate(this.specialAttackConfigs[epochIndex].projectile,
                new Vector3(Random.Range(this.specialAttackConfigs[epochIndex].minX, this.specialAttackConfigs[epochIndex].maxX), 15, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(this.specialAttackConfigs[epochIndex].minTimeBetweenSpawning, this.specialAttackConfigs[epochIndex].maxTimeBetweenSpawning));
        }
    }
}
