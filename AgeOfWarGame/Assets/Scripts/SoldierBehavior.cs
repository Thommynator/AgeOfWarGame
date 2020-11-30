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
    protected Vector3 relativAttackPosition;
    protected HealthBar healthBar;

    void Start()
    {
        this.body = GetComponent<Rigidbody2D>();
        this.soldierConfig = GetComponentInChildren<SoldierConfig>();
        this.currentStats = gameObject.AddComponent(typeof(CurrentStats)) as CurrentStats;
        this.currentStats.currentSpeed = this.soldierConfig.maxSpeed;
        this.currentStats.health = this.soldierConfig.health;
        this.timeOfPreviousAttack = 0;
        this.relativAttackPosition = this.transform.Find("Sprite").transform.localPosition;
        this.healthBar = GetComponentInChildren<HealthBar>();
        this.healthBar.SetMaxHealth(this.soldierConfig.health);
    }

    void FixedUpdate()
    {
        if (this.currentStats.health <= 0)
        {
            Die();
        }

        if (MeleeAttack() || RangeAttack())
        {
            StopWalking();
        }
        else
        {
            Walk();
        }
        this.healthBar.SetHealth(this.currentStats.health);
    }



    protected virtual void Walk()
    {
        // implemented in child
        return;
    }
    private void StopWalking()
    {
        this.body.velocity = Vector2.zero;
        this.body.angularVelocity = 0;
    }

    private void Die()
    {
        if (this.soldierConfig.isEnemy)
        {
            GameEvents.current.IncreaseMoney(this.soldierConfig.rewardMoney);
        }
        Destroy(this.gameObject);
    }

    protected virtual bool MeleeAttack()
    {
        // implemented in child
        return false;
    }

    protected virtual bool RangeAttack()
    {
        // implemented in child
        return false;
    }

    protected void WalkIntoDirection(int layerMask, Vector3 direction)
    {
        Vector2 newVelocity = (Vector2)direction * (speedFactor * this.soldierConfig.maxSpeed);
        int lookForwardDistance = 5;
        RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(transform.position.x, -1), (Vector2)direction, lookForwardDistance, layerMask);
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

        RaycastHit2D[] hits = Physics2D.RaycastAll((Vector2)GetAbsoluteAttackPosition(), direction, this.soldierConfig.meleeAttackRange, layerMask);

        foreach (RaycastHit2D hit in hits)
        {
            Debug.Log(hit.collider.gameObject.name);
        }

        if (hits.Length < 1)
        {
            return false;
        }

        if (Time.time - this.timeOfPreviousAttack > this.soldierConfig.attackCooldown)
        {
            foreach (RaycastHit2D hit in hits)
            {
                hit.collider.gameObject.GetComponent<CurrentStats>().TakeDamage(this.soldierConfig.strength);
            }
            this.timeOfPreviousAttack = Time.time;
        };
        return true;
    }

    protected bool RangeAttackOnLayer(int layerMask, Vector2 direction)
    {
        if (!soldierConfig.hasRangeAttack)
        {
            return false;
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll((Vector2)GetAbsoluteAttackPosition(), direction, this.soldierConfig.rangeAttackRange, layerMask);

        if (hits.Length < 1)
        {
            return false;
        }

        if (Time.time - this.timeOfPreviousAttack > this.soldierConfig.attackCooldown)
        {
            foreach (RaycastHit2D hit in hits)
            {
                hit.collider.gameObject.GetComponent<CurrentStats>().TakeDamage(this.soldierConfig.strength);
            }
            this.timeOfPreviousAttack = Time.time;
        };
        return true;
    }

    protected Vector3 GetAbsoluteAttackPosition()
    {
        return transform.position + this.relativAttackPosition;
    }



    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = new Color(1, 1, 1, 0.2F);
        if (this.soldierConfig.hasMeleeAttack)
        {
            Gizmos.DrawSphere(GetAbsoluteAttackPosition(), this.soldierConfig.meleeAttackRange);
        }
        if (this.soldierConfig.hasRangeAttack)
        {
            Gizmos.DrawSphere(GetAbsoluteAttackPosition(), this.soldierConfig.rangeAttackRange);
        }
    }
}


