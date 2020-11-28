using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAreaChecker : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Soldier")
        {
            GameEvents.current.PlayerSpawnAreaBlocked();
        }
        if (collider.gameObject.tag == "EnemySoldier")
        {
            GameEvents.current.EnemySpawnAreaBlocked();
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Soldier")
        {
            GameEvents.current.PlayerSpawnAreaBlocked();
        }
        if (collider.gameObject.tag == "EnemySoldier")
        {
            GameEvents.current.EnemySpawnAreaBlocked();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Soldier")
        {
            GameEvents.current.PlayerSpawnAreaFree();
        }
        if (collider.gameObject.tag == "EnemySoldier")
        {
            GameEvents.current.EnemySpawnAreaFree();
        }
    }
}
