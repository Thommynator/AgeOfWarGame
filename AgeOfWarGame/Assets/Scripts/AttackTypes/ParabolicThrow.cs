using UnityEngine;

public class ParabolicThrow : ProjectileAttack
{
    [Range(0f, 89f)]
    public int throwAngle = 45;
    private Rigidbody2D body;
    private Collider2D colliderObject;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        colliderObject = GetComponent<Collider2D>();
    }

    public override void AttackObject(GameObject target, float damage)
    {
        base.damage = damage;
        base.target = target;
        
        Vector2 s = (Vector2)target.transform.position - (Vector2)this.transform.position;

        bool isTargetToTheRight = s.x > 0;
        float alpha = isTargetToTheRight ? throwAngle * Mathf.Deg2Rad : -throwAngle * Mathf.Deg2Rad;
        float g = -Physics2D.gravity.y;
        float requiredSpeed = s.x / Mathf.Cos(alpha) * Mathf.Sqrt(g / (2 * (s.x * Mathf.Tan(alpha) - s.y)));
        body.velocity = new Vector2(Mathf.Cos(alpha) * requiredSpeed, Mathf.Sin(alpha) * requiredSpeed);
    }

}
