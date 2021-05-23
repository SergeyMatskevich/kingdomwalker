using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarModel
{
    public int maxHP;
    public int currentHP;
    public TileModel position;
    public TileModel previousPosition;
    public int movement;
    public MoveType moveType;
    public List<TileModel> availableTilesForMove;

    public AvatarModel(int lvl)
    {
        switch(lvl)
        {
            case 1:
                maxHP = 10;
                currentHP = 10;
                position = null;
                previousPosition = null;
                movement = 1;
                moveType = MoveType.SingleMove;
                availableTilesForMove = new List<TileModel>();
                break;
            case 2:
                maxHP = 10;
                currentHP = 10;
                position = null;
                previousPosition = null;
                movement = 1;
                moveType = MoveType.SingleMove;
                availableTilesForMove = new List<TileModel>();
                break;
            case 3:
                maxHP = 10;
                currentHP = 10;
                position = null;
                previousPosition = null;
                movement = 1;
                moveType = MoveType.SingleMove;
                availableTilesForMove = new List<TileModel>();
                break;
            case 4:
                maxHP = 10;
                currentHP = 10;
                position = null;
                previousPosition = null;
                movement = 1;
                moveType = MoveType.SingleMove;
                availableTilesForMove = new List<TileModel>();
                break;
            case 5:
                maxHP = 10;
                currentHP = 10;
                position = null;
                previousPosition = null;
                movement = 1;
                moveType = MoveType.SingleMove;
                availableTilesForMove = new List<TileModel>();
                break;
            case 6:
                maxHP = 10;
                currentHP = 10;
                position = null;
                previousPosition = null;
                movement = 1;
                moveType = MoveType.SingleMove;
                availableTilesForMove = new List<TileModel>();
                break;
            case 7:
                maxHP = 10;
                currentHP = 10;
                position = null;
                previousPosition = null;
                movement = 1;
                moveType = MoveType.SingleMove;
                availableTilesForMove = new List<TileModel>();
                break;
            case 8:
                maxHP = 10;
                currentHP = 10;
                position = null;
                previousPosition = null;
                movement = 1;
                moveType = MoveType.SingleMove;
                availableTilesForMove = new List<TileModel>();
                break;
            case 9:
                maxHP = 10;
                currentHP = 10;
                position = null;
                previousPosition = null;
                movement = 1;
                moveType = MoveType.SingleMove;
                availableTilesForMove = new List<TileModel>();
                break;
            case 10:
                maxHP = 10;
                currentHP = 10;
                position = null;
                previousPosition = null;
                movement = 1;
                moveType = MoveType.SingleMove;
                availableTilesForMove = new List<TileModel>();
                break;
            case 11:
                maxHP = 10;
                currentHP = 10;
                position = null;
                previousPosition = null;
                movement = 1;
                moveType = MoveType.SingleMove;
                availableTilesForMove = new List<TileModel>();
                break;
        }
    }

    public void PlusHitpoints(int heal)
    {
        if (currentHP + heal > maxHP)
        {
            currentHP = maxHP;
        }
        else
        {
            currentHP += heal;
        }

    }

    public void MinusHitpoints(int damage)
    {
        currentHP -= damage;
    }
}
