using System.Collections.Generic;
using UnityEngine;

public class EpochManager : MonoBehaviour {
    public static EpochManager current;
    public GameObject levelUpButton;

    [Header("Entities Per Epoch")]
    public List<SoldiersPerEpochConfig> soldiersOfAllEpochs;
    public List<TurretsPerEpochConfig> turretsOfAllEpochs;
    public List<SpecialAttackConfig> specialAttacksOfAllEpochs;

    [Header("Current Epoch")]
    public int playerEpoch;
    public int enemyEpoch;


    private void Awake() {
        current = this;
        this.playerEpoch = 0;
        this.enemyEpoch = 0;
        this.levelUpButton.SetActive(false);
    }

    void Update() {
        if (this.CanUpgradeToNextEpoch()) {
            this.levelUpButton.SetActive(true);
        } else {
            this.levelUpButton.SetActive(false);
        }
    }

    public List<GameObject> GetSoldiersOfCurrentPlayerEpoch() {
        return this.soldiersOfAllEpochs[this.playerEpoch].soldiersPerEpoch;
    }

    public List<GameObject> GetSoldiersOfCurrentEnemyEpoch() {
        return this.soldiersOfAllEpochs[this.enemyEpoch].soldiersPerEpoch;
    }

    public List<GameObject> GetTurretsOfCurrentPlayerEpoch() {
        return this.turretsOfAllEpochs[this.playerEpoch].turretsPerEpoch;
    }

    public List<GameObject> GetTurretsOfCurrentEnemyEpoch() {
        return this.turretsOfAllEpochs[this.enemyEpoch].turretsPerEpoch;
    }

    public SpecialAttackConfig GetSpecialAttackConfigOfCurrentPlayerEpoch() {
        return this.specialAttacksOfAllEpochs[this.playerEpoch];
    }

    public SpecialAttackConfig GetSpecialAttackConfigOfCurrentEnemyEpoch() {
        return this.specialAttacksOfAllEpochs[this.enemyEpoch];
    }

    public void EvolveToNextPlayerEpoch() {
        this.playerEpoch++;
    }

    private bool CanUpgradeToNextEpoch() {
        if (this.playerEpoch < this.soldiersOfAllEpochs.Count - 1) {
            int[] xpLimits = new int[] { 10, 2000, 5000, 15000 };
            int nextLimit = xpLimits[this.playerEpoch];
            return XpManager.current.xp >= nextLimit;
        }
        return false;
    }

}
