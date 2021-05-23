using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCardAction : CardAction
{
    

    public HealCardAction(int healPoints)
    {
        ActionType = ActionType.Heal;
        heal = healPoints;
    }

    public override void Invoke(PlayerModel owner, PlayerModel enemy)
    {
        owner.avatar.PlusHitpoints(heal);
    }
}
