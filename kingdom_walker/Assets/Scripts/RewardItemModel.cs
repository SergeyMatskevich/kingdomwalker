using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardItemModel
{
    public RewardItemType type;
    public int amount;

    public RewardItemModel(RewardItemType Type, int Amount)
    {
        type = Type;
        amount = Amount;
    }
}
