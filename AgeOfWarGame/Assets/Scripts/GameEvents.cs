using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    private void Awake()
    {
        current = this;
    }


    public event Action<int> onBuySoldier;
    public void BuySoldierTrigger(int soldierType)
    {
        if (onBuySoldier != null)
        {
            onBuySoldier(soldierType);
        }
    }

}
