using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentStats : MonoBehaviour
{
    public float health;
    public float currentSpeed;

    void Start()
    {

    }

    public void TakeDamage(float damage)
    {
        this.health -= damage;
    }
}
