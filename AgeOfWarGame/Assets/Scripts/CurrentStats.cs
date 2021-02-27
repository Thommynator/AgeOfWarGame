using UnityEngine;

public class CurrentStats : MonoBehaviour {
    public float health;
    public float currentSpeed;

    public void TakeDamage(float damage) {
        this.health -= damage;
    }
}
