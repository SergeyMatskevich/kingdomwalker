using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCardAction : CardAction
{
    //public int damage;

    public DamageCardAction(int damage)
    {
        this.ActionType = ActionType.DealDamage;
        this.damage = damage;
    }

    public override void Invoke(PlayerModel owner, PlayerModel enemy)
    {
        enemy.avatar.MinusHitpoints(damage);
    }
}
