using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEffectCardAction : CardAction
{
    //public MoveType moveType;

    public MoveEffectCardAction(MoveType moveType, int CoolDown = 0)
    {
        this.moveType = moveType;
        this.ActionType = ActionType.TransformMove;
        this.coolDown = CoolDown;
        this.coolDownLeft = 0;
    }

    public override void Invoke(PlayerModel owner, PlayerModel enemy)
    {
        owner.avatar.moveType = moveType;
        base.MoveInCooldown();
        
    }
}
