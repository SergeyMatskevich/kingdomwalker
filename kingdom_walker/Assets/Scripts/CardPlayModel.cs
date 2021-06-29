using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayModel
{
    public CardPlayType type;

    public CardPlayModel(CardPlayType Type = CardPlayType.Default)
    {
        type = Type;
    }
}
