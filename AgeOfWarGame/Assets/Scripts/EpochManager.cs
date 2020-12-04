using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpochManager : MonoBehaviour
{
    public static EpochManager current;
    public List<SoldiersPerEpochConfig> soldiersOfAllEpochs;
    public int playerEpoch;
    public int enemyEpoch;


    private void Awake()
    {
        current = this;
        playerEpoch = 0;
        enemyEpoch = 0;
    }

    public List<GameObject> GetSoldiersOfCurrentPlayerEpoch()
    {
        return soldiersOfAllEpochs[playerEpoch].soldiersPerEpoch;
    }

    public List<GameObject> GetSoldiersOfCurrentEnemyEpoch()
    {
        return soldiersOfAllEpochs[enemyEpoch].soldiersPerEpoch;
    }
}
