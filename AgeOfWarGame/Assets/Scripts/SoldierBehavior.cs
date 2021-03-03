using System.Collections.Generic;
using UnityEngine;

public class SoldierBehavior : MonoBehaviour {
    public SoldierConfig soldierConfig;
    public AudioClip[] painSounds;
    public GameObject earnerGameObject;
    private Rigidbody2D body;
    private CurrentStats currentStats;
    private float speedFactor = 0.2f;
    private float timeOfPreviousAttack;
    private Vector3 relativAttackPosition;
    private HealthBar healthBar;
    private Animator animator;
    private GameObject bloodSplashObject;

    [SerializeField]
    public List<RaycastHit2D> nextSoldiersToAttack;

    void Start() {

        if (!this.IsEnemy()) {
            this.soldierConfig = SkillTreeManager.current.GetSoldierConfigWithUpgrades(this.soldierConfig);
        }

        this.body = GetComponent<Rigidbody2D>();
        this.currentStats = gameObject.AddComponent(typeof(CurrentStats)) as CurrentStats;
        this.currentStats.currentSpeed = this.soldierConfig.maxSpeed;
        this.currentStats.health = this.soldierConfig.health;
        this.timeOfPreviousAttack = 0;
        this.relativAttackPosition = this.transform.Find("Body").transform.localPosition;
        this.earnerGameObject = GameObject.Find("Earner");
        this.healthBar = GetComponentInChildren<HealthBar>();
        this.healthBar.SetMaxHealth(this.soldierConfig.health);
        this.animator = GetComponent<Animator>();
        this.nextSoldiersToAttack = new List<RaycastHit2D>();
        this.bloodSplashObject = this.transform.Find("BloodSplash").gameObject;
    }

    void FixedUpdate() {
        if (this.currentStats.health <= 0) {
            Die();
        }

        if (PrepareAttack()) {
            StopWalking();
        } else {
            Walk();
        }
        this.healthBar.SetHealth(this.currentStats.health);
    }

    protected void Walk() {
        this.animator.SetBool("isWalking", true);
        if (IsEnemy()) {
            WalkIntoDirection(LayerMask.GetMask(new string[1] { "EnemySoldier" }), Vector3.left);
        } else {
            WalkIntoDirection(LayerMask.GetMask(new string[1] { "PlayerSoldier" }), Vector3.right);
        }
    }
    private void StopWalking() {
        this.animator.SetBool("isWalking", false);
        this.body.velocity = Vector2.zero;
        this.body.angularVelocity = 0;
    }

    private void Die() {
        if (IsEnemy()) {
            // Kill Reward
            GameEvents.current.IncreaseMoney(this.soldierConfig.rewardMoney);
            GameEvents.current.IncreaseXp(this.soldierConfig.rewardXp);

            GameObject earner = GameObject.Instantiate(earnerGameObject, transform.position, Quaternion.identity);
            earner.GetComponent<Earner>().Play();
        } else {
            // Soldier Cashback
            EconomyConfig economyConfig = SkillTreeManager.current.GetEconomyConfigWithUpgrades();
            if (economyConfig.relativeSoldierCashback > 0) {
                GameEvents.current.IncreaseMoney(Mathf.CeilToInt(this.soldierConfig.price * economyConfig.relativeSoldierCashback));
            }
        }

        if (painSounds.Length > 0) {
            AudioClip randomClip = painSounds[Random.Range(0, painSounds.Length)];
            AudioSource.PlayClipAtPoint(randomClip, new Vector3(transform.position.x, transform.position.y, -10));
        }
        Explode();
    }

    public void Explode() {
        bloodSplashObject.transform.SetParent(null);
        bloodSplashObject.transform.Find("ParticleSystem").GetComponent<ParticleSystem>().Play();
        Destroy(this.gameObject);
    }

    protected bool PrepareAttack() {
        // search for targets
        if (soldierConfig.attackType == SoldierConfig.AttackType.MELEE) {
            this.nextSoldiersToAttack = FindSoldiersToAttack(this.soldierConfig.meleeAttackRange);
        } else if (soldierConfig.attackType == SoldierConfig.AttackType.RANGE) {
            this.nextSoldiersToAttack = FindSoldiersToAttack(this.soldierConfig.rangeAttackRange);
        }

        if (this.nextSoldiersToAttack.Count < 1) {
            return false;
        }

        if (Time.time - this.timeOfPreviousAttack > this.soldierConfig.attackCooldown) {
            this.timeOfPreviousAttack = Time.time;
            this.animator.SetTrigger("attack");
        };
        return true;
    }

    /**
    This function is called by an animation event.
    */
    public void Attack() {

        // Moral 
        float moralAmplifier = 1;
        if (!IsEnemy() && this.soldierConfig.isMoralActive) {
            int soldiersOnBattlefield = GameObject.Find("PlayerSoldiers").transform.childCount;
            moralAmplifier += soldiersOnBattlefield / 20.0f; // 5% per soldier
            Debug.Log("Soldiers: " + soldiersOnBattlefield);
            Debug.Log("Moral: " + moralAmplifier);
        }
        float damage = this.soldierConfig.strength * moralAmplifier;

        foreach (RaycastHit2D hit in this.nextSoldiersToAttack) {
            if (this.soldierConfig.attackType == SoldierConfig.AttackType.MELEE) {
                // hitscan -> instant damage
                hit.collider.gameObject.GetComponent<CurrentStats>().TakeDamage(damage);
            } else if (this.soldierConfig.attackType == SoldierConfig.AttackType.RANGE) {
                // projectile
                GameObject projectile = GameObject.Instantiate(this.soldierConfig.rangeProjectile, this.transform.position + Vector3.up * 0.2f, Quaternion.identity);
                if (this.gameObject.tag == "PlayerSoldier") {
                    projectile.layer = LayerMask.NameToLayer("PlayerProjectile");
                } else if (this.gameObject.tag == "EnemySoldier") {
                    projectile.layer = LayerMask.NameToLayer("EnemyProjectile");
                }
                projectile.GetComponent<ProjectileAttack>().AttackObject(hit.collider.gameObject, damage);
            }

            if (!this.soldierConfig.canAttackMultiple) {
                break;
            }
        }
        this.nextSoldiersToAttack = new List<RaycastHit2D>();
    }


    protected void WalkIntoDirection(int layerMask, Vector3 direction) {
        Vector2 newVelocity = (Vector2)direction * (speedFactor * this.soldierConfig.maxSpeed);
        int lookForwardDistance = 5;
        RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(transform.position.x, -1), (Vector2)direction, lookForwardDistance, layerMask);
        if (hits.Length > 1) {
            ColliderDistance2D colliderDistance = Physics2D.Distance(hits[0].collider, hits[1].collider);
            float offsetThreshold = 0.5f;
            if (colliderDistance.distance < offsetThreshold) {
                newVelocity = hits[1].rigidbody.velocity;
            }
        }

        if (newVelocity.magnitude < 0.1f) {
            StopWalking();
        } else {
            this.body.angularVelocity = 0;
            body.velocity = Vector2.Lerp(body.velocity, newVelocity, 0.1f);
        }
    }

    protected List<RaycastHit2D> FindSoldiersToAttack(float attackRange) {
        Vector2 direction;
        int layerMask;
        if (IsEnemy()) {
            layerMask = LayerMask.GetMask(new string[2] { "PlayerSoldier", "PlayerBuilding" });
            direction = Vector2.left;
        } else {
            direction = Vector2.right;
            layerMask = LayerMask.GetMask(new string[2] { "EnemySoldier", "EnemyBuilding" });
        }
        RaycastHit2D[] hits = Physics2D.RaycastAll((Vector2)GetAbsoluteAttackPosition(), direction, attackRange, layerMask);
        return SortHitsByIncreasingDistance(hits);
    }

    private List<RaycastHit2D> SortHitsByIncreasingDistance(RaycastHit2D[] hits) {
        List<RaycastHit2D> sortedHits = new List<RaycastHit2D>(hits);
        sortedHits.Sort((x, y) => x.CompareTo(y));
        return sortedHits;
    }


    protected Vector3 GetAbsoluteAttackPosition() {
        return transform.position + this.relativAttackPosition;
    }

    void OnDrawGizmosSelected() {
        if (!Application.isPlaying) return;

        Gizmos.color = new Color(1, 1, 1, 0.2F);
        if (this.soldierConfig.attackType == SoldierConfig.AttackType.MELEE) {
            Gizmos.DrawSphere(GetAbsoluteAttackPosition(), this.soldierConfig.meleeAttackRange);
        }
        if (this.soldierConfig.attackType == SoldierConfig.AttackType.RANGE) {
            Gizmos.DrawSphere(GetAbsoluteAttackPosition(), this.soldierConfig.rangeAttackRange);
        }
    }

    private bool IsEnemy() {
        return this.gameObject.tag == "EnemySoldier";
    }
}


