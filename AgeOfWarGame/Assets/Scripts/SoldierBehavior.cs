using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBehavior : MonoBehaviour
{

    private Rigidbody2D body;
    private SoldierConfig soldierConfig;
    private CurrentStats currentStats;
    private float speedFactor = 0.1f;
    private float timeOfPreviousAttack;

    void Start()
    {
        this.body = GetComponent<Rigidbody2D>();
        this.soldierConfig = GetComponentInChildren<SoldierConfig>();
        this.currentStats = gameObject.AddComponent(typeof(CurrentStats)) as CurrentStats;
        this.currentStats.currentSpeed = this.soldierConfig.maxSpeed;
        this.currentStats.health = this.soldierConfig.health;
        this.timeOfPreviousAttack = 0;
    }

    void FixedUpdate()
    {
        Walk();
        MeleeAttack();
    }

    private void Walk()
    {
        Vector2 newVelocity = Vector2.right * (speedFactor * this.soldierConfig.maxSpeed);
        int targetLayerMask = LayerMask.GetMask(new string[2] { "PlayerSoldier", "EnemySoldier" });
        int lookForwardDistance = 5;
        Debug.DrawLine(new Vector3(transform.position.x, -1, 0), new Vector3(transform.position.x, -1, 0) + Vector3.right * lookForwardDistance, Color.blue);
        RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(transform.position.x, -1), Vector2.right, lookForwardDistance, targetLayerMask);
        if (hits.Length > 1)
        {
            ColliderDistance2D colliderDistance = Physics2D.Distance(hits[0].collider, hits[1].collider);
            float offsetThreshold = 0.5f;
            if (colliderDistance.distance < offsetThreshold)
            {
                newVelocity = hits[1].rigidbody.velocity;
            }
        }

        body.angularVelocity = 0;
        body.velocity = Vector2.Lerp(body.velocity, newVelocity, 0.1f);
    }

    private void MeleeAttack()
    {
        if (!soldierConfig.hasMeleeAttack)
        {
            return;
        }

        int layerMask = LayerMask.GetMask(new string[1] { "EnemySoldier" });
        RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(transform.position.x, -1), Vector2.right, this.soldierConfig.meleeAttackRange, layerMask);

        if (Time.time - this.timeOfPreviousAttack > this.soldierConfig.attackCooldown)
        {
            foreach (RaycastHit2D hit in hits)
            {
                Debug.Log("Deal damage to " + hit.collider.gameObject.name);
                hit.collider.gameObject.GetComponent<CurrentStats>().TakeDamage(this.soldierConfig.strength);
            }
            this.timeOfPreviousAttack = Time.time;
        };
    }

    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;
        Gizmos.color = new Color(1, 1, 1, 0.2F);
        if (this.soldierConfig.hasMeleeAttack)
        {
            Gizmos.DrawSphere(transform.position, this.soldierConfig.meleeAttackRange);
        }
        if (this.soldierConfig.hasRangeAttack)
        {
            Gizmos.DrawSphere(transform.position, this.soldierConfig.rangeAttackRange);
        }
    }
}


