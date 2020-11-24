using UnityEngine;
using System.Collections;

public class PlayerSoldier : SoldierBehavior
{
    protected override void Walk()
    {
        base.WalkIntoDirection(Vector3.right);
    }

    protected override bool MeleeAttack()
    {
        return base.MeleeAttackOnLayer(LayerMask.GetMask(new string[1] { "EnemySoldier" }), Vector2.right);
    }
}