using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour {
    public TurretConfig turretConfig;
    private float timeOfPreviousAttack;

    void Start() {
        this.timeOfPreviousAttack = 0;
    }

    void Update() {
        if (Time.time - this.timeOfPreviousAttack > this.turretConfig.attackCooldown) {
            List<Collider2D> enemieColliders = FindAllEnemiesInRange();
            if (enemieColliders.Count > 0) {
                if (this.turretConfig.canAttackMultiple) {
                    enemieColliders.ForEach(AttackColliderPosition);
                } else {
                    // TODO the first one is not always the closest one... not sure if I want that
                    AttackColliderPosition(enemieColliders[0]);
                }
                this.timeOfPreviousAttack = Time.time;
            }
        }
    }

    private void AttackColliderPosition(Collider2D collider) {
        GameObject projectile = Instantiate(this.turretConfig.projectile, this.transform);
        projectile.transform.SetParent(null);
        projectile.GetComponent<ProjectileAttack>().AttackObject(collider.gameObject, this.turretConfig.strength);
    }

    private List<Collider2D> FindAllEnemiesInRange() {
        List<Collider2D> enemieColliders = new List<Collider2D>();
        ContactFilter2D contactFilter = new ContactFilter2D();
        if (this.tag == "PlayerTurret") {
            contactFilter.SetLayerMask(LayerMask.GetMask(new string[] { "EnemySoldier" }));
        } else if (this.tag == "EnemyTurret") {
            contactFilter.SetLayerMask(LayerMask.GetMask(new string[] { "PlayerSoldier" }));
        }
        Physics2D.OverlapCircle(this.transform.position, this.turretConfig.attackRange, contactFilter, enemieColliders);
        return enemieColliders;
    }

    void OnDrawGizmosSelected() {
        if (!Application.isPlaying) return;

        Gizmos.color = new Color(1, 1, 1, 0.2F);
        Gizmos.DrawSphere(this.transform.position, this.turretConfig.attackRange);
    }
}
