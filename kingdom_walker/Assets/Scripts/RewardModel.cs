using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardModel
{
    public List<RewardItemModel> items;

    public RewardModel()
    {
        items = new List<RewardItemModel>();
    }

    public int GetCoins()
    {
        foreach (RewardItemModel model in items)
        {
            if (model.type == RewardItemType.Coin)
            {
                return model.amount;
            }
        }

        return 0;
    }
    
    public int GetGems()
    {
        foreach (RewardItemModel model in items)
        {
            if (model.type == RewardItemType.Gem)
            {
                return model.amount;
            }
        }

        return 0;
    }
}
