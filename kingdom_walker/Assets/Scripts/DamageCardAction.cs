using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCardAction : CardAction
{
    //public int damage;

    public DamageCardAction(int damage, int coolDown)
    {
        this.ActionType = ActionType.DealDamage;
        this.damage = damage;
        this.coolDown = coolDown;
        this.coolDownLeft = 0;
    }

    public override void Invoke(PlayerModel owner, PlayerModel enemy)
    {
        enemy.avatar.MinusHitpoints(damage);
        base.MoveInCooldown();
    }
}
