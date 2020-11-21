using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBehavior : MonoBehaviour
{

    private SoldierConfig soldierConfig;

    private float speedFactor = 0.1f;
    void Start()
    {
        soldierConfig = GetComponentInChildren<SoldierConfig>();
    }

    void FixedUpdate()
    {
        if (soldierConfig.isEnemy)
        {
            transform.position -= new Vector3(speedFactor * soldierConfig.speed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.position += new Vector3(speedFactor * soldierConfig.speed * Time.deltaTime, 0, 0);

        }
    }
}
