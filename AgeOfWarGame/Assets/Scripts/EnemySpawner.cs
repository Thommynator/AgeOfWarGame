using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject meleeSoldier;
    public float meleeWeightProbability;
    public GameObject rangeSoldier;
    public float rangeWeightProbability;
    public GameObject tankSoldier;
    public float tankWeightProbability;
    public float minimumSpawnCooldown;

    private float nextSpawnCooldown;
    private float weightProbabilitySum;
    private Vector3 spawnPosition;
    private GameObject enemySoldiers;
    private bool isSpawnAreaFree;
    private float timeOfPreviousSpawn;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onEnemySpawnAreaFree += () => { isSpawnAreaFree = true; };
        GameEvents.current.onEnemySpawnAreaBlocked += () => { isSpawnAreaFree = false; };

        this.weightProbabilitySum = meleeWeightProbability + rangeWeightProbability + tankWeightProbability;
        this.spawnPosition = new Vector3(15, 0, 0);
        this.enemySoldiers = GameObject.Find("EnemySoldiers");
        this.isSpawnAreaFree = true;
        this.timeOfPreviousSpawn = Time.time;
        this.nextSpawnCooldown = NextSpawnCooldown();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawnAreaFree && Time.time - timeOfPreviousSpawn > nextSpawnCooldown)
        {
            PickRandomSoldier();
        }
    }

    private void PickRandomSoldier()
    {
        float randomNumber = Random.Range(0, weightProbabilitySum);
        if (randomNumber >= 0 && randomNumber < meleeWeightProbability)
        {
            // spawn melee solider
            SpawnSoldier(meleeSoldier);
        }
        else if (randomNumber >= meleeWeightProbability && randomNumber < meleeWeightProbability + rangeWeightProbability)
        {
            // spawn range soldier
            SpawnSoldier(rangeSoldier);
        }
        else if (randomNumber >= meleeWeightProbability + rangeWeightProbability && randomNumber < meleeWeightProbability + rangeWeightProbability + tankWeightProbability)
        {
            // spawn tank soldier
            SpawnSoldier(tankSoldier);
        }
    }

    private void SpawnSoldier(GameObject nextSoldier)
    {
        GameObject soldier = GameObject.Instantiate(nextSoldier, this.spawnPosition, Quaternion.identity);
        soldier.transform.SetParent(enemySoldiers.transform);
        this.timeOfPreviousSpawn = Time.time;
        this.nextSpawnCooldown = NextSpawnCooldown();
    }

    private float NextSpawnCooldown()
    {
        return this.minimumSpawnCooldown * Random.Range(1.0f, 3.0f);
    }
}
