using UnityEngine;

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
        Vector2 s = (Vector2)target.transform.position - (Vector2)this.transform.position;
        float alpha = 45 * Mathf.Deg2Rad;
        float g = -Physics2D.gravity.y;
        float requiredSpeed = s.x / Mathf.Cos(alpha) * Mathf.Sqrt(g / (2 * (s.x * Mathf.Tan(alpha) - s.y)));
        body.velocity = new Vector2(Mathf.Cos(alpha) * requiredSpeed, Mathf.Sin(alpha) * requiredSpeed);
    }

}
