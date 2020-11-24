using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBehavior : MonoBehaviour
{
    protected Rigidbody2D body;
    protected SoldierConfig soldierConfig;
    protected CurrentStats currentStats;
    protected float speedFactor = 0.1f;
    protected float timeOfPreviousAttack;

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
        if (!MeleeAttack())
        {
            Walk();
            Debug.Log("Walk" + gameObject.name);
        }
    }

    protected virtual void Walk()
    {
        // implemented in child
        return;
    }

    protected virtual bool MeleeAttack()
    {
        // implemented in child
        return false;
    }

    protected void WalkIntoDirection(Vector3 direction)
    {
        Vector2 newVelocity = (Vector2)direction * (speedFactor * this.soldierConfig.maxSpeed);
        int targetLayerMask = LayerMask.GetMask(new string[2] { "PlayerSoldier", "EnemySoldier" });
        int lookForwardDistance = 5;
        Debug.DrawLine(new Vector3(transform.position.x, -1, 0), new Vector3(transform.position.x, -1, 0) + direction * lookForwardDistance, Color.blue);
        RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(transform.position.x, -1), (Vector2)direction, lookForwardDistance, targetLayerMask);
        if (hits.Length > 1)
        {
            ColliderDistance2D colliderDistance = Physics2D.Distance(hits[0].collider, hits[1].collider);
            float offsetThreshold = 0.5f;
            if (colliderDistance.distance < offsetThreshold)
            {
                newVelocity = hits[1].rigidbody.velocity;
            }
        }

        this.body.angularVelocity = 0;
        body.velocity = Vector2.Lerp(body.velocity, newVelocity, 0.1f);
    }

    protected bool MeleeAttackOnLayer(int layerMask, Vector2 direction)
    {
        if (!soldierConfig.hasMeleeAttack)
        {
            return false;
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(transform.position.x, -1), direction, this.soldierConfig.meleeAttackRange, layerMask);

        if (hits.Length < 1)
        {
            return false;
        }

        if (Time.time - this.timeOfPreviousAttack > this.soldierConfig.attackCooldown)
        {
            foreach (RaycastHit2D hit in hits)
            {
                Debug.Log("Deal damage to " + hit.collider.gameObject.name);
                hit.collider.gameObject.GetComponent<CurrentStats>().TakeDamage(this.soldierConfig.strength);
            }
            this.timeOfPreviousAttack = Time.time;
        };
        return true;
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


