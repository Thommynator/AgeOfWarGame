using UnityEngine;

public class HorizontalThrow : ProjectileAttack
{
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
        Vector2 differenceToTarget = (Vector2)target.transform.position - (Vector2)this.transform.position;

        // horizontal throw can not go higher than its start position
        if (differenceToTarget.y < 0)
        {
            float requiredSpeedInX = differenceToTarget.x / (Mathf.Sqrt(2 * -differenceToTarget.y / -Physics.gravity.y));
            body.velocity = new Vector2(requiredSpeedInX, 0);
        }
    }

}
