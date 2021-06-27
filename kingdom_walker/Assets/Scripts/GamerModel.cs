using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamerModel
{
    public int coins;
    public int gems;
    public int level;

    public GamerModel(int Coins = 0, int Gems = 0, int Level = 1)
    {
        coins = Coins;
        gems = Gems;
        level = Level;
    }
}
