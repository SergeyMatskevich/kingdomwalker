using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public bool isPlayer;
    public bool isActive;
    public AvatarModel avatar;
    public List<CardModel> deck;
    public CardModel activeCard;
    public List<CardModel> availableCards;
    public int deckCapacity;
    

    public PlayerModel(bool IsPlayer, TileModel startTile,  List<CardModel> Deck = null, int DeckCapacity = 0)
    {
        isPlayer = IsPlayer;
        avatar = new AvatarModel(startTile);
        deck = Deck;
        activeCard = null;
        availableCards = new List<CardModel>();
        deckCapacity = DeckCapacity;
        isActive = false;
    }

    public void SelectRandomCard()
    {
        int rand = Random.Range(0, deck.Count);

        activeCard = deck[rand];
    }
}
