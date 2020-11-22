using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBehavior : MonoBehaviour
{

    private Rigidbody2D body;
    private SoldierConfig soldierConfig;

    private float speedFactor = 0.1f;

    private float currentSpeed;

    private Vector2 start;

    private Vector2 end;

    void Start()
    {
        this.body = GetComponent<Rigidbody2D>();
        this.soldierConfig = GetComponentInChildren<SoldierConfig>();
        this.currentSpeed = this.soldierConfig.maxSpeed;

        start = new Vector2(0.5f, 0);
        end = new Vector2(0.8f, 0);
    }

    void FixedUpdate()
    {

        Walk();
    }

    private void Walk()
    {
        Vector2 newVelocity = Vector2.right * (speedFactor * this.soldierConfig.maxSpeed);

        int targetLayerMask = LayerMask.GetMask(new string[2] { "PlayerSoldier", "EnemySoldier" });
        int lookForwardDistance = 5;
        Debug.DrawLine(new Vector3(transform.position.x, -1, 0), new Vector3(transform.position.x, -1, 0) + Vector3.right * lookForwardDistance, Color.blue);
        RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(transform.position.x, -1), Vector2.right, lookForwardDistance, targetLayerMask);
        if (hits.Length > 1)
        {
            ColliderDistance2D colliderDistance = Physics2D.Distance(hits[0].collider, hits[1].collider);
            float offsetThreshold = 0.5f;
            if (colliderDistance.distance < offsetThreshold)
            {
                newVelocity = hits[1].rigidbody.velocity;
            }
        }

        body.angularVelocity = 0;
        body.velocity = Vector2.Lerp(body.velocity, newVelocity, 0.1f);
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Gizmos.color = new Color(1, 1, 1, 0.2F);
        Gizmos.DrawSphere(transform.position, this.soldierConfig.attackRange);
    }
}


