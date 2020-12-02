using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpManager : MonoBehaviour
{

    public int xp;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onIncreaseXp += (int newXp) => this.xp += newXp;
        GameEvents.current.onDecreaseXp += (int newXp) => this.xp -= newXp;
    }

}
