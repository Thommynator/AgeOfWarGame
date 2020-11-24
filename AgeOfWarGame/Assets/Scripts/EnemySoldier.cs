using UnityEngine;
using System.Collections;

public class EnemySoldier : SoldierBehavior
{
    protected override void Walk()
    {
        base.WalkIntoDirection(Vector3.left);
    }

    protected override bool MeleeAttack()
    {
        return base.MeleeAttackOnLayer(LayerMask.GetMask(new string[1] { "PlayerSoldier" }), Vector2.left);
    }
}