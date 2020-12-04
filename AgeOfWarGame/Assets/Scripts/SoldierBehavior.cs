using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBehavior : MonoBehaviour
{
    public SoldierConfig soldierConfig;
    protected Rigidbody2D body;
    protected CurrentStats currentStats;
    protected float speedFactor = 0.2f;
    protected float timeOfPreviousAttack;
    protected Vector3 relativAttackPosition;
    protected HealthBar healthBar;

    void Start()
    {
        this.body = GetComponent<Rigidbody2D>();
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



    protected void Walk()
    {
        if (isEnemy())
        {
            WalkIntoDirection(LayerMask.GetMask(new string[1] { "EnemySoldier" }), Vector3.left);
        }
        else
        {
            WalkIntoDirection(LayerMask.GetMask(new string[1] { "PlayerSoldier" }), Vector3.right);
        }
    }
    private void StopWalking()
    {
        this.body.velocity = Vector2.zero;
        this.body.angularVelocity = 0;
    }

    private void Die()
    {
        if (isEnemy())
        {
            GameEvents.current.IncreaseMoney(this.soldierConfig.rewardMoney);
            GameEvents.current.IncreaseXp(this.soldierConfig.rewardXp);
        }
        Destroy(this.gameObject);
    }

    protected bool MeleeAttack()
    {
        if (isEnemy())
        {
            return MeleeAttackOnLayer(LayerMask.GetMask(new string[2] { "PlayerSoldier", "PlayerBuilding" }), Vector2.left);
        }
        else
        {
            return MeleeAttackOnLayer(LayerMask.GetMask(new string[2] { "EnemySoldier", "EnemyBuilding" }), Vector2.right);
        }

    }

    protected virtual bool RangeAttack()
    {
        if (isEnemy())
        {
            return RangeAttackOnLayer(LayerMask.GetMask(new string[2] { "PlayerSoldier", "PlayerBuilding" }), Vector2.left);
        }
        else
        {
            return RangeAttackOnLayer(LayerMask.GetMask(new string[2] { "EnemySoldier", "EnemyBuilding" }), Vector2.right);
        }
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
        if (soldierConfig.attackType != SoldierConfig.AttackType.MELEE && soldierConfig.attackType != SoldierConfig.AttackType.BOTH)
        {
            return false;
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll((Vector2)GetAbsoluteAttackPosition(), direction, this.soldierConfig.meleeAttackRange, layerMask);

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
        if (soldierConfig.attackType != SoldierConfig.AttackType.RANGE && soldierConfig.attackType != SoldierConfig.AttackType.BOTH)
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
            List<RaycastHit2D> sortedHits = SortHitsByIncreasingDistance(hits);
            foreach (RaycastHit2D hit in sortedHits)
            {
                hit.collider.gameObject.GetComponent<CurrentStats>().TakeDamage(this.soldierConfig.strength);
                if (!this.soldierConfig.canAttackMultiple)
                {
                    break;
                }
            }
            this.timeOfPreviousAttack = Time.time;
        };
        return true;
    }

    private List<RaycastHit2D> SortHitsByIncreasingDistance(RaycastHit2D[] hits)
    {
        List<RaycastHit2D> sortedHits = new List<RaycastHit2D>(hits);
        sortedHits.Sort((x, y) => x.CompareTo(y));
        return sortedHits;
    }


    protected Vector3 GetAbsoluteAttackPosition()
    {
        return transform.position + this.relativAttackPosition;
    }

    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = new Color(1, 1, 1, 0.2F);
        if (this.soldierConfig.attackType == SoldierConfig.AttackType.MELEE || this.soldierConfig.attackType == SoldierConfig.AttackType.BOTH)
        {
            Gizmos.DrawSphere(GetAbsoluteAttackPosition(), this.soldierConfig.meleeAttackRange);
        }
        if (this.soldierConfig.attackType == SoldierConfig.AttackType.RANGE || this.soldierConfig.attackType == SoldierConfig.AttackType.BOTH)
        {
            Gizmos.DrawSphere(GetAbsoluteAttackPosition(), this.soldierConfig.rangeAttackRange);
        }
    }

    private bool isEnemy()
    {
        return this.gameObject.tag == "EnemySoldier";
    }
}


