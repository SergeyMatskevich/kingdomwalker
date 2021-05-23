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

    public TileModel(int X, int Y)
    {
        x = X;
        y = Y;
    }
}
