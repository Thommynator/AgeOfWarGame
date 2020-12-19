using System.Collections;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{

    protected float damage;

    public virtual void AttackObject(GameObject target, float damage)
    {
        // Implemented by the child
        return;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (PlayerProjectileHitEnemy(collision) || EnemyProjectileHitPlayer(collision))
        {
            collision.collider.gameObject.GetComponent<CurrentStats>().TakeDamage(this.damage);
        }
        Destroy(this.gameObject);
    }


    protected bool PlayerProjectileHitEnemy(Collision2D collision)
    {
        return this.gameObject.tag == "PlayerTurretProjectile" && collision.collider.gameObject.tag == "EnemySoldier";
    }

    protected bool EnemyProjectileHitPlayer(Collision2D collision)
    {
        return this.gameObject.tag == "EnemyTurretProjectile" && collision.collider.gameObject.tag == "PlayerSoldier";
    }
}
