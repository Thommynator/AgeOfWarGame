using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.gameObject.name);
        if (collision.collider.gameObject.tag == "EnemySoldier")
        {
            collision.collider.gameObject.GetComponent<CurrentStats>().TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }
}
