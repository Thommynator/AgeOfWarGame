using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    public TurretConfig turretConfig;
    private float timeOfPreviousAttack;

    void Start()
    {
        this.timeOfPreviousAttack = 0;
    }

    void Update()
    {
        if (Time.time - this.timeOfPreviousAttack > this.turretConfig.attackCooldown)
        {
            GameObject projectile = Instantiate(this.turretConfig.projectile, this.transform);
            projectile.transform.SetParent(null);
            projectile.GetComponent<ProjectileAttack>().AttackPosition(new Vector2(-10, -2), this.turretConfig.strength);
            this.timeOfPreviousAttack = Time.time;
        };
    }


    private List<Collider2D> FindAllEnemiesInRange()
    {
        List<Collider2D> enemieColliders = new List<Collider2D>();
        // Physics2D.OverlapCircle(this.transform.position, this.turretConfig.attackRange, );

        return enemieColliders;
    }

    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = new Color(1, 1, 1, 0.2F);
        Gizmos.DrawSphere(this.transform.position, this.turretConfig.attackRange);
    }
}
