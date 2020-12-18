using System.Collections;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{

    protected float damage;

    public virtual void AttackPosition(Vector2 targetPosition, float damage)
    {
        // Implemented by the child
        return;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "EnemySoldier")
        {
            collision.collider.gameObject.GetComponent<CurrentStats>().TakeDamage(this.damage);
        }
        Destroy(this.gameObject);
    }
}
