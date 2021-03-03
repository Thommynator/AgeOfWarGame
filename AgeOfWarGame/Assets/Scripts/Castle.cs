using UnityEngine;

public class Castle : MonoBehaviour {
    private HealthBar healthBar;
    private CurrentStats currentStats;

    void Start() {
        this.currentStats = gameObject.AddComponent(typeof(CurrentStats)) as CurrentStats;
        this.currentStats.health = 200;
        this.healthBar = GetComponentInChildren<HealthBar>();
        this.healthBar.SetMaxHealth(this.currentStats.health);
    }

    void Update() {
        this.healthBar.SetHealth(this.currentStats.health);
        if (this.currentStats.health <= 0) {
            if (this.tag == "PlayerBuilding") {
                GameEvents.current.GameOver();
            } else if (this.tag == "EnemyBuilding") {
                GameEvents.current.GameWin();
            }
        }
    }
}
