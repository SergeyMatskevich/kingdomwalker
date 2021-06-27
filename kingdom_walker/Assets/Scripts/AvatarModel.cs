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

    public AvatarModel(TileModel startTile, int HP = 10)
    {
        maxHP = HP;
        currentHP = HP;
        position = startTile;
        previousPosition = null;
        movement = 1;
        moveType = MoveType.SingleMove;

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
    
    public void SetAvatarMoves(List<PlayerModel> players, List<TileModel> tiles )
    {
        foreach (TileModel tile in tiles)
        {
            if (ApplyMoveType(tile) && !tile.IsOccupied(players))
            {
                tile.availableForMove = true;
            }
        }
    }
    
    private bool ApplyMoveType(TileModel tile)
    {
        int absoluteDifference = Mathf.Abs(position.x - tile.x) +
                                 Mathf.Abs(position.y - tile.y);

        bool result = false;

        switch (moveType)
        {
            case MoveType.SingleMove:

                result = absoluteDifference == movement;
                break;
            case MoveType.RollingRoll:
                result = absoluteDifference != 0 && absoluteDifference <= 2;
                break;
            case MoveType.HorseTrainer:
                result = absoluteDifference != 0 && absoluteDifference <= 3 &&
                         (Mathf.Abs(position.x - tile.x) == 2 && Mathf.Abs(position.y - tile.y) == 1
                          || Mathf.Abs(position.x - tile.x) == 1 && Mathf.Abs(position.y - tile.y) == 2);
                break;
            case MoveType.CabCarl:
                result = absoluteDifference != 0 && absoluteDifference <= 2
                                                 && (tile.x == position.x || tile.y == position.y);
                break;
            case MoveType.Bishop:
                result = absoluteDifference != 0 && absoluteDifference <= 4 &&
                         (Mathf.Abs(position.x - tile.x) == 1 && Mathf.Abs(position.y - tile.y) == 1
                          || Mathf.Abs(position.x - tile.x) == 2 && Mathf.Abs(position.y - tile.y) == 2);
                break;
        }

        return result;
    }

    public void MoveAvatar(TileModel tile)
    {
        previousPosition = position;
        position = tile;
    }

}
