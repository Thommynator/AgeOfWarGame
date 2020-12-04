using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Soldiers Per Epoch Config", menuName = "ScriptableObjects/SoldiersPerEpoch", order = 1)]
public class SoldiersPerEpochConfig : ScriptableObject
{
    public List<GameObject> soldiersPerEpoch;

}