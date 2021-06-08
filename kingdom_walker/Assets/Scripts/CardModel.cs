using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel
{
    public string name;
    public bool canPlay;
    public CardAction action;
    public ConditionType conditionForActionByEnemy;
    public ConditionType conditionForActionByPlayer;
    public int coolDown;
    public int coolDownLeft;

    public CardModel(string Name, CardAction Action, ConditionType ByEnemy = ConditionType.Any, int CoolDown = 0, ConditionType ByPlayer = ConditionType.Any )
    {
        name = Name;
        action = Action;
        conditionForActionByEnemy = ByEnemy;
        conditionForActionByPlayer = ByPlayer;
        coolDown = CoolDown;
    }

    public bool CardFromPlayerDeck(PlayerModel player)
    {
        foreach (CardModel card in player.deck)
        {
            if (card == this)
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckCondition(PlayerModel owner, PlayerModel enemy)
    {
        if (conditionForActionByEnemy == ConditionType.EnemyOnTileTypeWater)
        {
            return enemy.avatar.position.tileType == TileType.Water;
        }

        if (conditionForActionByEnemy == ConditionType.EnemyOnTileTypePlains)
        {
            return enemy.avatar.position.tileType == TileType.Plain;
        }

        if (conditionForActionByEnemy == ConditionType.EnemyOnTileTypeForest)
        {
            return enemy.avatar.position.tileType == TileType.Forest;
        }

        else if (conditionForActionByEnemy == ConditionType.Any)
        {
            return true;
        }

        return false;
    }

}
