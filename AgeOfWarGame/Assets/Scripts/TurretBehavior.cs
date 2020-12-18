using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    public TurretConfig turretConfig;

    private float timeOfPreviousAttack;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Ignore collision between " + LayerMask.NameToLayer("PlayerSoldier") + " and " + LayerMask.NameToLayer("PlayerTurretProjectile"));
        // Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerSoldier"), LayerMask.NameToLayer("PlayerTurretProjectile"));
        this.timeOfPreviousAttack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - this.timeOfPreviousAttack > this.turretConfig.attackCooldown)
        {
            GameObject projectile = Instantiate(this.turretConfig.projectile, this.transform);
            projectile.GetComponent<ProjectileAttack>().AttackPosition(new Vector2(-7.57f, -1.95f));
            this.timeOfPreviousAttack = Time.time;
        };
    }


    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = new Color(1, 1, 1, 0.2F);
        Gizmos.DrawSphere(this.transform.position, this.turretConfig.attackRange);
    }
}
