using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    public PlayerModel player;
    public PlayerModel enemy;
    public MapModel map;

    public GameModel(int lvl = 10)
    {
        player = new PlayerModel(lvl,true);// true means player is player
        enemy = new PlayerModel(lvl,false);
        map = new MapModel(lvl);
    }
}
