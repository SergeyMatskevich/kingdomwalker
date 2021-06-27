using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardAction
{
    public ActionType ActionType;
    public MoveType moveType;
    public int damage;
    public int heal;
    public int coolDown;
    public int coolDownLeft;

    public abstract void Invoke(PlayerModel owner, PlayerModel enemy);

    public void MoveInCooldown()
    {
        if (coolDown > 0)
        {
            coolDownLeft = coolDown;    
        }
    }
}
