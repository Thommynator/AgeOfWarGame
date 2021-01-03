using UnityEngine;

public class SpecialAttackDamage : MonoBehaviour
{
    public SpecialAttackConfig specialAttackConfig;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isOpposingTeam(collision))
        {
            collision.collider.gameObject.GetComponent<CurrentStats>().TakeDamage(specialAttackConfig.damage);
        }
        Destroy(this.gameObject);
    }

    private bool isOpposingTeam(Collision2D collision)
    {
        bool playerHitEnemy = this.gameObject.tag == "PlayerSoldier" && collision.collider.gameObject.tag == "EnemySoldier";
        bool enemyHitPlayer = this.gameObject.tag == "EnemySoldier" && collision.collider.gameObject.tag == "PlayerSoldier";
        return playerHitEnemy || enemyHitPlayer;
    }
}
