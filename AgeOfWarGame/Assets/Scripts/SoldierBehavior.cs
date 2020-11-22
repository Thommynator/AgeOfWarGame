using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBehavior : MonoBehaviour
{

    private Rigidbody2D body;
    private SoldierConfig soldierConfig;

    private float speedFactor = 0.1f;

    private float currentSpeed;

    void Start()
    {
        this.body = GetComponent<Rigidbody2D>();
        this.soldierConfig = GetComponentInChildren<SoldierConfig>();
        this.currentSpeed = this.soldierConfig.maxSpeed;
    }

    void FixedUpdate()
    {
        Walk();
        // GetAllCollidersInAttackRange(this.soldierConfig.attackRange);
    }

    private void Walk()
    {

        body.angularVelocity = 0;
        Vector2 offset = Vector2.right * speedFactor * this.currentSpeed * Time.deltaTime;
        Debug.Log(offset);
        body.MovePosition((Vector2)transform.position + offset);
    }

    // private List<Collider2D> GetSoldierCollidersInRange(float attackRange)
    // {
    //     int targetLayerMask = LayerMask.GetMask(new string[2] { "PlayerSoldier", "EnemySoldier" });
    //     Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, attackRange, targetLayerMask);

    //     List<Collider2D> list = new List<Collider2D>(colliders.Length);
    //     foreach (Collider2D collider in colliders)
    //     {
    //         if (collider.gameObject != this.gameObject)
    //         {
    //             list.Add(collider);
    //             Debug.DrawLine(this.transform.position, collider.transform.position, Color.blue);
    //         }
    //     }
    //     return list;
    // }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.tag == "Soldier" && IsCollisionInFront(collision))
        {
            float speedInFront = collision.gameObject.GetComponent<Rigidbody2D>().velocity.x;
            if (speedInFront < 0.5f)
            {
                this.currentSpeed = 0;
                body.Sleep();
            }
            else
            {
                body.WakeUp();
                this.currentSpeed = speedInFront;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Soldier" && IsCollisionInFront(collision))
        {
            body.WakeUp();
            currentSpeed = this.soldierConfig.maxSpeed;
        }
    }

    private bool IsCollisionInFront(Collision2D collision)
    {
        return collision.gameObject.transform.position.x - this.transform.position.x > 0;
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Gizmos.color = new Color(1, 1, 1, 0.2F);
        Gizmos.DrawSphere(transform.position, this.soldierConfig.attackRange);
    }
}


