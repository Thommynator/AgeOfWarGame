using System.Collections;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    protected float damage;
    protected GameObject target;

    public virtual void AttackObject(GameObject target, float damage)
    {
        // Implemented by the child
        return;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerSoldier"
            || collision.gameObject.tag == "PlayerBuilding"
            || collision.gameObject.tag == "EnemySoldier"
            || collision.gameObject.tag == "EnemyBuilding")
        {
            collision.collider.gameObject.GetComponent<CurrentStats>().TakeDamage(this.damage);
        }
        Destroy(this.gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(target.transform.position, 0.5f);
    }
}
