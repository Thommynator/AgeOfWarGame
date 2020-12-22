using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turrets Per Epoch Config", menuName = "ScriptableObjects/TurretsPerEpochConfig", order = 1)]
public class TurretsPerEpochConfig : ScriptableObject
{
    public List<GameObject> turretsPerEpoch;
}