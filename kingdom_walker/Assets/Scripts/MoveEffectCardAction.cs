using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEffectCardAction : CardAction
{
    public MoveType moveType;

    public MoveEffectCardAction(MoveType moveType)
    {
        this.moveType = moveType;
        this.ActionType = ActionType.TransformMove;
    }

    public override void Invoke(PlayerModel owner, PlayerModel enemy)
    {
        owner.avatar.moveType = moveType;
    }
}
