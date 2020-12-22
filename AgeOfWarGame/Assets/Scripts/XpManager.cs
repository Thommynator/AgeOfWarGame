using UnityEngine;

public class XpManager : MonoBehaviour
{
    public int xp;

    void Start()
    {
        GameEvents.current.onIncreaseXp += (int newXp) => this.xp += newXp;
        GameEvents.current.onDecreaseXp += (int newXp) => this.xp -= newXp;
    }

}
