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

    public override void AttackPosition(Vector2 targetPosition)
    {
        Vector2 differenceToTarget = targetPosition - (Vector2)this.transform.position;
        float requiredSpeedInX = differenceToTarget.x / (Mathf.Sqrt(2 * -differenceToTarget.y / -Physics.gravity.y));
        body.AddForce(new Vector2(requiredSpeedInX, 0), ForceMode2D.Impulse);
    }

}
