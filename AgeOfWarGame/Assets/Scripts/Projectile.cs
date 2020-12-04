using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public SpecialAttackConfig specialAttackConfig;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.gameObject.name);
        if (collision.collider.gameObject.tag == "EnemySoldier")
        {
            collision.collider.gameObject.GetComponent<CurrentStats>().TakeDamage(specialAttackConfig.damage);
        }
        Destroy(this.gameObject);
    }
}
