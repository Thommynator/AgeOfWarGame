using UnityEngine;
using System.Collections;

public class TargetingShot : ProjectileAttack
{
    private Rigidbody2D body;
    private Collider2D colliderObject;
    private GameObject target;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        colliderObject = GetComponent<Collider2D>();
    }

    public override void AttackObject(GameObject target, float damage)
    {
        base.damage = damage;
        this.target = target;

        Vector2 s = (Vector2)target.transform.position - (Vector2)this.transform.position;
        float alpha = 85 * Mathf.Deg2Rad;
        float initialSpeed = 15;
        this.body.velocity = new Vector2(Mathf.Cos(alpha) * initialSpeed, Mathf.Sin(alpha) * initialSpeed);
        StartCoroutine(Target());
    }

    private IEnumerator Target()
    {
        // if the projectile is still rising, just wait
        while (this.body.velocity.y > 0)
        {
            yield return new WaitForSeconds(0.1f);
        }

        // when it starts to fall down, move incremental to target
        while (true)
        {
            Vector2 moveToPosition = Vector2.MoveTowards(this.transform.position, this.target.transform.position, 0.2f);
            this.body.MovePosition(moveToPosition);
            yield return new WaitForSeconds(0.01f);
        }

    }

}
