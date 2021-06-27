using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public GameObject cardPrefab;

    public GameObject cardOrdinaryPlace;

    public List<GameObject> playerDeck;
    public List<GameObject> enemyDeck;

    //public List<CardModel> availableCards;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitCardsObjects(int lvl, PlayerModel player)
    {
        List<CardModel> cards = new List<CardModel>();
        /*
        switch (lvl)
        {
            case 1:
                CardModel card11 = new CardModel("Bishop", new MoveEffectCardAction(MoveType.Bishop), ConditionType.Any);
                CardModel card12 = new CardModel("Cab Carl", new MoveEffectCardAction(MoveType.CabCarl), ConditionType.Any);
                cards.Add(card11);
                cards.Add(card12);
                break;
            case 2:
                CardModel card21 = new CardModel("Bishop", new MoveEffectCardAction(MoveType.Bishop), ConditionType.Any);
                CardModel card22 = new CardModel("Cab Carl", new MoveEffectCardAction(MoveType.CabCarl), ConditionType.Any);
                CardModel card23 = new CardModel("Horse Trainer", new MoveEffectCardAction(MoveType.HorseTrainer), ConditionType.Any);
                cards.Add(card21);
                cards.Add(card22);
                cards.Add(card23);
                break;
            case 3:
                CardModel card31 = new CardModel("Bishop", new MoveEffectCardAction(MoveType.Bishop), ConditionType.Any);
                CardModel card32 = new CardModel("Cab Carl", new MoveEffectCardAction(MoveType.CabCarl), ConditionType.Any);
                CardModel card33 = new CardModel("Horse Trainer", new MoveEffectCardAction(MoveType.HorseTrainer), ConditionType.Any);
                cards.Add(card31);
                cards.Add(card32);
                cards.Add(card33);
                break;
            case 4:
                CardModel card41 = new CardModel("Bishop", new MoveEffectCardAction(MoveType.Bishop), ConditionType.Any);
                CardModel card42 = new CardModel("Cab Carl", new MoveEffectCardAction(MoveType.CabCarl), ConditionType.Any);
                CardModel card43 = new CardModel("Horse Trainer", new MoveEffectCardAction(MoveType.HorseTrainer), ConditionType.Any);
                CardModel card44 = new CardModel("Goatherd", new DamageCardAction(6), ConditionType.EnemyOnTileTypePlains);
                cards.Add(card41);
                cards.Add(card42);
                cards.Add(card43);
                cards.Add(card44);
                break;
            case 5:
                CardModel card51 = new CardModel("Bishop", new MoveEffectCardAction(MoveType.Bishop), ConditionType.Any);
                CardModel card52 = new CardModel("Cab Carl", new MoveEffectCardAction(MoveType.CabCarl), ConditionType.Any);
                CardModel card53 = new CardModel("Horse Trainer", new MoveEffectCardAction(MoveType.HorseTrainer), ConditionType.Any);
                CardModel card54 = new CardModel("Log Thrower", new DamageCardAction(6), ConditionType.EnemyOnTileTypeForest);
                cards.Add(card51);
                cards.Add(card52);
                cards.Add(card53);
                cards.Add(card54);
                break;
            case 6:
                CardModel card61 = new CardModel("Bishop", new MoveEffectCardAction(MoveType.Bishop), ConditionType.Any);
                CardModel card62 = new CardModel("Cab Carl", new MoveEffectCardAction(MoveType.CabCarl), ConditionType.Any);
                CardModel card63 = new CardModel("Horse Trainer", new MoveEffectCardAction(MoveType.HorseTrainer), ConditionType.Any);
                CardModel card64 = new CardModel("Fisherman", new DamageCardAction(6), ConditionType.EnemyOnTileTypeWater);
                cards.Add(card61);
                cards.Add(card62);
                cards.Add(card63);
                cards.Add(card64);
                break;
            case 7:
                CardModel card71 = new CardModel("Horse Trainer", new MoveEffectCardAction(MoveType.HorseTrainer), ConditionType.Any);
                CardModel card72 = new CardModel("Fisherman", new DamageCardAction(6), ConditionType.EnemyOnTileTypeWater);
                CardModel card73 = new CardModel("Log Thrower", new DamageCardAction(6), ConditionType.EnemyOnTileTypeForest);
                CardModel card74 = new CardModel("Goatherd", new DamageCardAction(6), ConditionType.EnemyOnTileTypePlains);
                cards.Add(card71);
                cards.Add(card72);
                cards.Add(card73);
                cards.Add(card74);
                break;
            case 8:
                CardModel card81 = new CardModel("Bishop", new MoveEffectCardAction(MoveType.Bishop), ConditionType.Any);
                CardModel card82 = new CardModel("Cab Carl", new MoveEffectCardAction(MoveType.CabCarl), ConditionType.Any);
                CardModel card83 = new CardModel("Horse Trainer", new MoveEffectCardAction(MoveType.HorseTrainer), ConditionType.Any);
                CardModel card84 = new CardModel("Goatherd", new DamageCardAction(6), ConditionType.EnemyOnTileTypePlains);
                CardModel card85 = new CardModel("Healer Tom", new HealCardAction(3), ConditionType.Any);
                cards.Add(card81);
                cards.Add(card82);
                cards.Add(card83);
                cards.Add(card84);
                cards.Add(card85);
                break;
            case 9:
                CardModel card91 = new CardModel("Bishop", new MoveEffectCardAction(MoveType.Bishop), ConditionType.Any);
                CardModel card92 = new CardModel("Cab Carl", new MoveEffectCardAction(MoveType.CabCarl), ConditionType.Any);
                CardModel card93 = new CardModel("Horse Trainer", new MoveEffectCardAction(MoveType.HorseTrainer), ConditionType.Any);
                CardModel card94 = new CardModel("Log Thrower", new DamageCardAction(6), ConditionType.EnemyOnTileTypeForest);
                CardModel card95 = new CardModel("Healer Tom", new HealCardAction(3), ConditionType.Any);
                cards.Add(card91);
                cards.Add(card92);
                cards.Add(card93);
                cards.Add(card94);
                cards.Add(card95);
                break;
            case 10:
                CardModel card101 = new CardModel("Bishop", new MoveEffectCardAction(MoveType.Bishop), ConditionType.Any);
                CardModel card102 = new CardModel("Cab Carl", new MoveEffectCardAction(MoveType.CabCarl), ConditionType.Any);
                CardModel card103 = new CardModel("Horse Trainer", new MoveEffectCardAction(MoveType.HorseTrainer), ConditionType.Any);
                CardModel card104 = new CardModel("Fisherman", new DamageCardAction(6), ConditionType.EnemyOnTileTypeWater);
                CardModel card105 = new CardModel("Healer Tom", new HealCardAction(3), ConditionType.Any);
                cards.Add(card101);
                cards.Add(card102);
                cards.Add(card103);
                cards.Add(card104);
                cards.Add(card105);
                break;
            case 11:
                CardModel card111 = new CardModel("Bishop",new MoveEffectCardAction(MoveType.Bishop),ConditionType.Any);
                CardModel card112 = new CardModel("Cab Carl", new MoveEffectCardAction(MoveType.CabCarl), ConditionType.Any);
                CardModel card113 = new CardModel("Horse Trainer", new MoveEffectCardAction(MoveType.HorseTrainer), ConditionType.Any);
                CardModel card114 = new CardModel("Healer Tom", new HealCardAction(3), ConditionType.Any);
                CardModel card115 = new CardModel("Fisherman", new DamageCardAction(6), ConditionType.EnemyOnTileTypeWater);
                CardModel card116 = new CardModel("Log Thrower", new DamageCardAction(6), ConditionType.EnemyOnTileTypeForest);
                CardModel card117 = new CardModel("Goatherd", new DamageCardAction(6), ConditionType.EnemyOnTileTypePlains);
                cards.Add(card111);
                cards.Add(card112);
                cards.Add(card113);
                cards.Add(card114);
                cards.Add(card115);
                cards.Add(card116);
                cards.Add(card117);
                break;
        }
    
        player.availableCards = cards;
        */
    }

    public void SetCardsInDeck(PlayerModel player)
    {
        List<CardModel> temp = new List<CardModel>();

        foreach (CardModel card in player.availableCards)
        {
            temp.Add(card);
        }

        for (int i = 0; i < player.deckCapacity; i++)
        {
            int rand = Random.Range(0, temp.Count);
            player.deck.Add(temp[rand]);
            temp.RemoveAt(rand);
        }
    }

    public CardView IterateList(List<GameObject> list, PlayerModel player)
    {
        foreach (GameObject cardUI in list)
        {
            if (cardUI.GetComponent<CardView>().model == player.activeCard)
            {
                return cardUI.GetComponent<CardView>();
            }
        }

        return null;
    }
}
