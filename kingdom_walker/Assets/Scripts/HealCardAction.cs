using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCardAction : CardAction
{
    
    public HealCardAction(int healPoints, int coolDown)
    {
        ActionType = ActionType.Heal;
        heal = healPoints;
        this.coolDown = coolDown;
        this.coolDownLeft = 0;
    }

    public override void Invoke(PlayerModel owner, PlayerModel enemy)
    {
        owner.avatar.PlusHitpoints(heal);
        base.MoveInCooldown();
    }
}
