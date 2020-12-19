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
        collision.collider.gameObject.GetComponent<CurrentStats>().TakeDamage(this.damage);
        Destroy(this.gameObject);
    }
}
