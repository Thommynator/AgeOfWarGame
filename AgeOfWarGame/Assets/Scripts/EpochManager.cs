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

    private bool isLevelUpButtonVisible;

    private void Awake() {
        current = this;
        this.playerEpoch = 0;
        this.enemyEpoch = 0;
        this.levelUpButton.transform.localScale = Vector3.zero;
        this.isLevelUpButtonVisible = false;
    }

    void Update() {
        VisualizeLevelUpButton();
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
        GameEvents.current.DecreasecreaseXp(GetXpCostsForNextEpoch());
        this.playerEpoch++;
    }

    private void VisualizeLevelUpButton() {
        if (!this.isLevelUpButtonVisible && this.CanUpgradeToNextEpoch()) {
            this.isLevelUpButtonVisible = true;
            LeanTween.scale(this.levelUpButton, Vector3.one, 2f)
                .setEaseOutBounce()
                .setIgnoreTimeScale(true)
                .setOnComplete(() =>
                    LeanTween.scale(this.levelUpButton, Vector3.one * 1.1f, 0.3f).setIgnoreTimeScale(true).setLoopPingPong()
                );

        } else if (!this.CanUpgradeToNextEpoch()) {
            LeanTween.cancel(this.levelUpButton);
            this.isLevelUpButtonVisible = false;
            this.levelUpButton.transform.localScale = Vector3.zero;
        }
    }

    private bool CanUpgradeToNextEpoch() {
        if (this.playerEpoch < this.soldiersOfAllEpochs.Count - 1) {
            return XpManager.current.xp >= GetXpCostsForNextEpoch();
        }
        return false;
    }

    private int GetXpCostsForNextEpoch() {
        int[] xpLimits = new int[] { 10, 2000, 5000, 15000 };
        return xpLimits[this.playerEpoch];
    }

}
