using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float minimumSpawnCooldown;
    private float nextSpawnCooldown;
    private Vector3 spawnPosition;
    private GameObject enemySoldiers;
    private bool isSpawnAreaFree;
    private float timeOfPreviousSpawn;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onEnemySpawnAreaFree += () => { isSpawnAreaFree = true; };
        GameEvents.current.onEnemySpawnAreaBlocked += () => { isSpawnAreaFree = false; };

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
        List<GameObject> soldiersOfCurrentEpoch = EpochManager.current.GetSoldiersOfCurrentEnemyEpoch();
        List<float> weightRanges = new List<float>(soldiersOfCurrentEpoch.Count + 1);
        weightRanges.Add(0);
        float weightProbabilitySum = 0.0f;
        foreach (GameObject soldier in soldiersOfCurrentEpoch)
        {
            float weight = CalculateProbabilityWeight(soldier);
            weightProbabilitySum += weight;
            weightRanges.Add(weightProbabilitySum);
        }

        float randomNumber = Random.Range(0, weightProbabilitySum);

        for (int i = 0; i < soldiersOfCurrentEpoch.Count; i++)
        {
            if (randomNumber >= weightRanges[i] && randomNumber < weightRanges[i + 1])
            {
                SpawnSoldier(soldiersOfCurrentEpoch[i]);
                break;
            }
        }
    }

    private float CalculateProbabilityWeight(GameObject soldier)
    {
        SoldierConfig soldierConfig = soldier.GetComponent<SoldierBehavior>().soldierConfig;
        float weight = 100.0f / (soldierConfig.health + soldierConfig.price);
        Debug.Log(soldier.gameObject.name + " has a probability weight of " + weight);
        return weight;
    }

    private void SpawnSoldier(GameObject nextSoldier)
    {
        GameObject soldier = GameObject.Instantiate(nextSoldier, this.spawnPosition, Quaternion.identity);
        soldier.transform.localScale = new Vector3(-1, 1, 1);
        soldier.transform.Find("HealthBarCanvas").Find("HealthBar").localScale = new Vector3(-1, 1, 1);
        soldier.tag = "EnemySoldier";
        soldier.layer = LayerMask.NameToLayer("EnemySoldier");
        soldier.transform.SetParent(enemySoldiers.transform);
        this.timeOfPreviousSpawn = Time.time;
        this.nextSpawnCooldown = NextSpawnCooldown();
    }

    private float NextSpawnCooldown()
    {
        return this.minimumSpawnCooldown * Random.Range(1.0f, 3.0f);
    }
}
