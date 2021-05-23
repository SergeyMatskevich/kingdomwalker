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

    public void PopulateDeckOnUI(PlayerModel player)
    {
        Vector3 initDeckPosition;
        string deckName = "Player";
        float x = 0;
        float offset = 2.25F;
        float xDeck = -4.5F;
        float yDeck = -9F;
        float yCardOrd = 2.8F;

        if (!player.isPlayer)
        {
            deckName = "Enemy";
            yDeck = 9F;
            yCardOrd = -2.8f;
        }

        if (player.deck.Count == 5)
        {
            x = 0F;
            //xDeck = -4.5F;
            //initDeckPosition = new Vector3(-4.5F, -9F, 0F);
        }
        else if (player.deck.Count == 4)
        {
            x = 1.125F;
            ///xDeck = -3.375F;
            //initDeckPosition = new Vector3(-3.375F, -9F, 0F);
        }
        else if (player.deck.Count == 3)
        {
            x = 2.25F;
            //xDeck = -2.25F;
            //initDeckPosition = new Vector3(-2.25F, -9F, 0F);
        }
        else
        {
            x = 3.375F;
            //xDeck = -1.125F;
            //initDeckPosition = new Vector3(-1.125F, -9F, 0F);
        }

        initDeckPosition = new Vector3(xDeck,yDeck);

        RectTransform gameHolder = GameObject.Find("GameController").GetComponent<RectTransform>();
        RectTransform deckHolder = new GameObject("Deck" + deckName, typeof(RectTransform)).GetComponent<RectTransform>();

        foreach (CardModel card in player.deck)
        {
            //Debug.Log(x);

            GameObject instance = Instantiate(cardPrefab, new Vector3(x,0F,0F), Quaternion.identity);
            instance.transform.SetParent(deckHolder);
            instance.transform.localScale = new Vector3(0.85F, 0.85F);
            //instance.transform.localPosition = new Vector3(offset * x, 0F, 0F)
            if (player.isPlayer)
            {
                playerDeck.Add(instance);
            }
            else
            {
                enemyDeck.Add(instance);
            }

            instance.GetComponent<CardView>().model = card;
            instance.GetComponent<CardView>().SetSprites();
            instance.GetComponent<CardView>().startPlace = new Vector3(x, 0F, 0F);
            instance.GetComponent<CardView>().ordinaryPlace = new Vector3(4.5F, yCardOrd, 0F);
            x += offset;
        }

        GameObject instanceCardOrdinary = Instantiate(cardOrdinaryPlace, new Vector3(4.5F, yCardOrd, 0F), Quaternion.identity);
        instanceCardOrdinary.transform.SetParent(deckHolder);
        instanceCardOrdinary.transform.localScale = new Vector3(1F, 1F);
        //instance.transform.localPosition = new Vector3(offset * x, 0F, 0F)
        if (player.isPlayer)
        {
            playerDeck.Add(instanceCardOrdinary);
        }
        else
        {
            enemyDeck.Add(instanceCardOrdinary);
        }

        deckHolder.transform.SetParent(gameHolder);

        //deckHolder.localScale = new Vector3(1.83F, 1.83F);
        deckHolder.position = initDeckPosition;

        // 5 : -4.5 - 9 , dist 2.25
        // 3 : -2.25 - 9, dist 2.25
        // 4 : -3.375 -9, dist 2.25 

        //Draw deck on ui
        //Take deck from player model
    }

    public void SelectPlayerCard(PlayerModel player)
    {
        int rand = Random.Range(0, player.deck.Count);

        player.activeCard = player.deck[rand];

        player.deck[rand].onTable = true;
    }

    public void MoveCardOnUI(PlayerModel player)
    {
        if (player.isPlayer)
        {
            IterateList(playerDeck, player).MoveToOrdinaryPlace(player);
        }
        else
        {
            IterateList(enemyDeck, player).MoveToOrdinaryPlace(player);
        }
    }

    public void MoveCardOnUIBack(PlayerModel player)
    {
        CardView view;

        if (player.isPlayer)
        {
            view = IterateList(playerDeck, player);
            ResetOnTabel(false, player);
            view.ActionIdle();
            view.MoveToPlace(player);
            //IterateList(playerDeck, player).MoveToPlace(player);
        }
        else
        {
            view = IterateList(enemyDeck, player);
            ResetOnTabel(false, player);
            view.ActionIdle();
            view.MoveToPlace(player);
        }
    }

    public void ResetOnTabel(bool state, PlayerModel player)
    {
        foreach (CardModel model in player.deck)
        {
            model.onTable = state;
        }
    }

    public void CardSuccessAnimation(PlayerModel player)
    {
        if (player.isPlayer)
        {
            IterateList(playerDeck, player).SuccessAction();
        }
        else
        {
            IterateList(enemyDeck, player).SuccessAction();
        }
    }

    public void CardFailAnimation(PlayerModel player)
    {
        if (player.isPlayer)
        {
            IterateList(playerDeck, player).FailAction();
        }
        else
        {
            IterateList(enemyDeck, player).FailAction();
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

    public void InvokeCard(PlayerModel player, PlayerModel enemy)
    {
        if (player.activeCard != null && player.activeCard.CheckCondition(player, enemy))
        {
            player.activeCard.action.Invoke(player, enemy);
            player.activeCard.onTable = false;
        }

        AnimateCardAction(player, enemy);

    }

    public void AnimateCardAction(PlayerModel player, PlayerModel enemy)
    {
        if (player.activeCard != null)
        {
            if (player.activeCard.action.ActionType == ActionType.TransformMove)
            {
                CardSuccessAnimation(player);
            }
            if (player.activeCard.action.ActionType == ActionType.DealDamage)
            {
                if (player.activeCard.CheckCondition(player, enemy))
                {
                    CardSuccessAnimation(player);
                    GameController._gC._pC.InstantiateAttack(player);
                    GameController._gC._pC.UpdateHealth(enemy);
                    GameController._gC._pC.InstantiateFloatingText(enemy, "-" + player.activeCard.action.damage, new Color(1F, 0F, 0F));
                    // RUN -HP
                    // RUN HP update
                }
                else
                {
                    CardFailAnimation(player);
                    //GameController.gC.pC.InstantiateAttack(player);
                }
            }
            if (player.activeCard.action.ActionType == ActionType.Heal)
            {
                CardSuccessAnimation(player);
                GameController._gC._pC.InstantiateHeal(player);
                GameController._gC._pC.UpdateHealth(player);
                GameController._gC._pC.InstantiateFloatingText(player, "+" + player.activeCard.action.heal, new Color(0F, 1F, 0F));
                // RUN +HP
                // RUN HP update
            }

            //player.activeCard = null;
        }
    }
}
