using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAreaChecker : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Soldier")
        {
            GameEvents.current.SpawnAreaBlocked();
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Soldier")
        {
            GameEvents.current.SpawnAreaBlocked();
        }
    }

    void OnTriggerExit2D()
    {
        GameEvents.current.SpawnAreaFree();
    }
}
