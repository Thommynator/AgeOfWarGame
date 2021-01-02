using UnityEngine;

public class SpecialAttackDamage : MonoBehaviour
{
    public SpecialAttackConfig specialAttackConfig;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "EnemySoldier")
        {
            collision.collider.gameObject.GetComponent<CurrentStats>().TakeDamage(specialAttackConfig.damage);
        }
        Destroy(this.gameObject);
    }
}
