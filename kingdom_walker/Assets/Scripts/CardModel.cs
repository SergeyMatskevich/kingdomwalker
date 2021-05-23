using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel
{
    public string name;
    public bool onTable;
    public CardAction action;
    public ConditionType conditionForAction;

    public CardModel(string Name, CardAction Action, ConditionType ConditionForAction)
    {
        name = Name;
        action = Action;
        conditionForAction = ConditionForAction;
    }

    public bool CheckCondition(PlayerModel owner, PlayerModel enemy)
    {
        if (conditionForAction == ConditionType.EnemyOnTileTypeWater)
        {
            return enemy.avatar.position.tileType == TileType.Water;
        }

        if (conditionForAction == ConditionType.EnemyOnTileTypePlains)
        {
            return enemy.avatar.position.tileType == TileType.Plain;
        }

        if (conditionForAction == ConditionType.EnemyOnTileTypeForest)
        {
            return enemy.avatar.position.tileType == TileType.Forest;
        }

        else if (conditionForAction == ConditionType.Any)
        {
            return true;
        }

        return false;
    }

}
