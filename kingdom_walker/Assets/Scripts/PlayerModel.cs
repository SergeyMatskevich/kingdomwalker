using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public bool isPlayer;
    public AvatarModel avatar;
    public List<CardModel> deck;
    public CardModel activeCard;
    public List<CardModel> availableCards;
    public int deckCapacity;
    

    public PlayerModel(int lvl, bool IsPlayer)
    {
        isPlayer = IsPlayer;
        avatar = new AvatarModel(lvl);
        deck = new List<CardModel>();
        activeCard = null;
        availableCards = new List<CardModel>();
        switch (lvl)
        {
            case 11:
                deckCapacity = 5;
                break;
            case 1:
                deckCapacity = 2;
                break;
            case 2:
                deckCapacity = 3;
                break;
            case 3:
                deckCapacity = 3;
                break;
            case 4:
                deckCapacity = 4;
                break;
            case 5:
                deckCapacity = 4;
                break;
            case 6:
                deckCapacity = 4;
                break;
            case 7:
                deckCapacity = 4;
                break;
            case 8:
                deckCapacity = 5;
                break;
            case 9:
                deckCapacity = 5;
                break;
            case 10:
                deckCapacity = 5;
                break;
        }
    }
}
