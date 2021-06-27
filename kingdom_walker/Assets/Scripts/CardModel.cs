using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel
{
    public string name;
    public bool canPlay;
    public CardAction action;
    public CardAction upgradedAction;
    public CardAction activeAction;
    public ConditionType conditionForActionByEnemy;
    public ConditionType conditionForActionByPlayer;
    public bool isUpgradable;
    public bool isUpgraded;
    public bool isPlayed;
    public int upgradeCost;
    public int cooldownRemoveCost;

    public CardModel(string Name, CardAction Action, ConditionType ByEnemy = ConditionType.Any, 
                        ConditionType ByPlayer = ConditionType.Any, bool IsUpgradable = false, int UpgradeCost = 0, 
                        int CooldownRemoveCost = 0, CardAction UpgradedAction = null, bool IsPlayed = false)
    {
        name = Name;
        action = Action;
        conditionForActionByEnemy = ByEnemy;
        conditionForActionByPlayer = ByPlayer;
        isUpgradable = IsUpgradable;
        isPlayed = IsPlayed;
        upgradeCost = UpgradeCost;
        cooldownRemoveCost = CooldownRemoveCost;
        upgradedAction = UpgradedAction;
        
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

    public void InvokeAction(PlayerModel player, PlayerModel target)
    {
        player.activeCard = this;
        action.Invoke(player,target);
        isPlayed = true;
    }

    public void Unfreeze()
    {
        action.coolDownLeft = 0;
    }

    public void MinusCooldown()
    {
        if (CardIsInCooldown())
        {
            action.coolDownLeft -= 1;
        }
        
        if (action.coolDownLeft > 0)
        {
            GameController._gC.OnCardInCooldown?.Invoke(this);    
        }
        else
        {
            GameController._gC.OnCardRefreshed?.Invoke(this);
        }
    }

    public bool CardIsInCooldown()
    {
        if (action.coolDownLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
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
