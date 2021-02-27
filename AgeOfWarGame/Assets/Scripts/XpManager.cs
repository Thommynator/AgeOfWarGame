using UnityEngine;

public class XpManager : MonoBehaviour {

    public static XpManager current;
    public int xp;
    public ParticleSystem decreaseXpPS;

    private void Awake() {
        current = this;
    }

    void Start() {
        GameEvents.current.onIncreaseXp += (int newXp) => this.xp += newXp;
        GameEvents.current.onDecreaseXp += (int newXp) => {
            this.xp -= newXp;
            this.decreaseXpPS.Play();
        };
    }

}
