using System.Collections.Generic;
using UnityEngine;

public class EpochManager : MonoBehaviour
{
    public static EpochManager current;
    public List<SoldiersPerEpochConfig> soldiersOfAllEpochs;
    public List<TurretsPerEpochConfig> turretsOfAllEpochs;
    public List<SpecialAttackConfig> specialAttacksOfAllEpochs;

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

    public List<GameObject> GetTurretsOfCurrentPlayerEpoch()
    {
        return turretsOfAllEpochs[playerEpoch].turretsPerEpoch;
    }

    public List<GameObject> GetTurretsOfCurrentEnemyEpoch()
    {
        return turretsOfAllEpochs[enemyEpoch].turretsPerEpoch;
    }

    public SpecialAttackConfig GetSpecialAttackConfigOfCurrentPlayerEpoch()
    {
        return specialAttacksOfAllEpochs[playerEpoch];
    }

    public SpecialAttackConfig GetSpecialAttackConfigOfCurrentEnemyEpoch()
    {
        return specialAttacksOfAllEpochs[enemyEpoch];
    }
}
