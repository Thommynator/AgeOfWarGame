using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{

    private HealthBar healthBar;

    private CurrentStats currentStats;

    // Start is called before the first frame update
    void Start()
    {
        this.currentStats = gameObject.AddComponent(typeof(CurrentStats)) as CurrentStats;
        this.currentStats.health = 200;
        this.healthBar = GetComponentInChildren<HealthBar>();
        this.healthBar.SetMaxHealth(this.currentStats.health);
    }

    // Update is called once per frame
    void Update()
    {
        this.healthBar.SetHealth(this.currentStats.health);
        if (this.currentStats.health < 0)
        {
            GameEvents.current.GameOver();
        }
    }
}
