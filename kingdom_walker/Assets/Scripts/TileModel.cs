using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileModel 
{
    public int x;
    public int y;
    public TileType tileType;
    public bool availableForMove;
    public float distance;
    public TileView view;

    public TileModel(int X, int Y)
    {
        x = X;
        y = Y;
    }

    public TileModel(int X, int Y, TileType type)
    {
        x = X;
        y = Y;
        tileType = type;
    }
    
    public bool IsOccupied(List<PlayerModel> players)
    {
        foreach (PlayerModel player in players)
        {
            if (player.avatar.position == this)
            {
                return true;
            }
        }
        return false;
    }

    public void UpdateUI()
    {
        view.UpdateAnimation();
    }
}
