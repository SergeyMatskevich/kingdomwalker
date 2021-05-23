using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardAction
{
    public ActionType ActionType;
    public int damage;
    public int heal;

    public abstract void Invoke(PlayerModel owner, PlayerModel enemy);
}
