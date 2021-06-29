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
    public GamerModel gamer;
    public List<CardPlayModel> cardPlays;
    

    public PlayerModel(bool IsPlayer, TileModel startTile,  List<CardModel> Deck = null, int DeckCapacity = 0, 
                            GamerModel Gamer = null)
    {
        isPlayer = IsPlayer;
        avatar = new AvatarModel(startTile);
        deck = Deck;
        activeCard = null;
        availableCards = new List<CardModel>();
        deckCapacity = DeckCapacity;
        isActive = false;
        gamer = Gamer; 
        cardPlays = new List<CardPlayModel>();
        cardPlays.Add(new CardPlayModel());
    }

    public void RefreshDeck()
    {
        activeCard = null;
        if (deck != null)
        {
            foreach (CardModel card in deck)
            {
                card.isPlayed = false;
                card.MinusCooldown();
            }    
        }
    }

}
