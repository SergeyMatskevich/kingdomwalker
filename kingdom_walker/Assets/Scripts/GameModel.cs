using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    public int _sequenceNumber;
    public int _turnNumber;
    public MapModel _map = new MapModel();
    public List<PlayerModel> _players;
    public List<CardModel> _availableCards;
    public List<TutorialMessageModel> _tutorialMessageModels;
    public RewardModel _winReward;
    public RewardModel _loseReward;
    

    public GameModel(int lvl = 10)
    {
        _turnNumber = 0;
        _sequenceNumber = lvl;
        _players = new List<PlayerModel>();
        _availableCards = new List<CardModel>();
        _tutorialMessageModels = new List<TutorialMessageModel>();
        _winReward = new RewardModel();
        _loseReward = new RewardModel();
        
        switch (lvl)
        {
            case 1:
                _map = new MapModel(3, 3, 3, 1, 3, 2, 2, 0, 0);
                _players.Add(new PlayerModel(true,GetTilePerCoordinates(0,0),null,0,GameController._gC.gamer));
                //_players.Add(new PlayerModel(true, GetTilePerCoordinates(0, 0), null, 0, GameController._gC.gamer));
                _tutorialMessageModels.Add(new TutorialMessageModel(0,"TAP SHAKING TILE TO MOVE!",ScreenLocation.Bottom, AdditionalElement.arrow,AdditionalElementPlace.moveTile));
                _tutorialMessageModels.Add(new TutorialMessageModel(1,"GOOD! YOU NEED TO REACH <color=#FF0000>RED CASTLE</color> TO GET REWARD!",ScreenLocation.Top, AdditionalElement.arrow,AdditionalElementPlace.redCastleTile));
                _winReward.items.Add(new RewardItemModel(RewardItemType.Coin, 30));
                _winReward.items.Add(new RewardItemModel(RewardItemType.Gem, 3));
                break;
            case 2:
                _map = new MapModel(4, 3, 0, 0, 10, 2, 2, 0, 0);
                List<CardModel> playerDeck2 = new List<CardModel>();
                playerDeck2.Add(new CardModel("Sword",new DamageCardAction(5,0),ConditionType.Any,ConditionType.Any));
                List<CardModel> enemyDeck2  = new List<CardModel>();
                enemyDeck2.Add(new CardModel("PlainsRook",new MoveEffectCardAction(MoveType.CabCarl),ConditionType.Any));
                _players.Add(new PlayerModel(true, GetTilePerCoordinates(0,0),playerDeck2,1,GameController._gC.gamer));
                _players.Add(new PlayerModel(false, GetTilePerCoordinates(_map._columns - 1,_map._rows - 1 ),enemyDeck2,1));
                _tutorialMessageModels.Add(new TutorialMessageModel(0,"YOU ARE NOT ALONE! <color=#FF0000>ENEMY</color> WANTS TO CAPTURE <color=#0000FF>YOUR CASTLE</color>!",ScreenLocation.Bottom, AdditionalElement.arrow, AdditionalElementPlace.enemy));
                _tutorialMessageModels.Add(new TutorialMessageModel(2,"TRY TO PLAY CARDS BEFORE MAKE MOVE",ScreenLocation.Top, AdditionalElement.arrow, AdditionalElementPlace.playerCard));
                _winReward.items.Add(new RewardItemModel(RewardItemType.Coin, 30));
                _winReward.items.Add(new RewardItemModel(RewardItemType.Gem, 3));
                _loseReward.items.Add(new RewardItemModel(RewardItemType.Coin, 5 ));
                break;
            case 3:
                _map = new MapModel(5, 5, 15, 3, 3, 3, 3, 0, 0);
                List<CardModel> playerDeck3 = new List<CardModel>();
                playerDeck3.Add(new CardModel("Sword",new DamageCardAction(4,6),ConditionType.Any,ConditionType.Any,true,10,0,new DamageCardAction(6,6)));
                List<CardModel> enemyDeck3 = new List<CardModel>();
                //enemyDeck3.Add(new CardModel("PlainsRook",new MoveEffectCardAction(MoveType.CabCarl,5),ConditionType.Any,ConditionType.Any));
                enemyDeck3.Add(new CardModel("PlainsBishop",new MoveEffectCardAction(MoveType.Bishop,2),ConditionType.Any));
                _players.Add(new PlayerModel(true, GetTilePerCoordinates(0,0),playerDeck3,1,GameController._gC.gamer));
                _players.Add(new PlayerModel(false, GetTilePerCoordinates(_map._columns - 1,_map._rows - 1 ),enemyDeck3,1));
                _tutorialMessageModels.Add(new TutorialMessageModel(0,"Cards might have <color=#00A6B0>cooldown</color> time! After being played, your card will not be available for <color=#00A6B0>6</color> turns! Play it and make move!",ScreenLocation.Bottom, AdditionalElement.arrow, AdditionalElementPlace.playerCard));
                _tutorialMessageModels.Add(new TutorialMessageModel(2,"Luckily we can unfreeze card. Press <color=#B600FF>Unfreeze button</color> and keep attack enemy!",ScreenLocation.Bottom, AdditionalElement.arrow, AdditionalElementPlace.playerCard));
                _tutorialMessageModels.Add(new TutorialMessageModel(4,"Keep unfreezing card to win!",ScreenLocation.Top, AdditionalElement.arrow, AdditionalElementPlace.playerCard));
                _winReward.items.Add(new RewardItemModel(RewardItemType.Coin, 30));
                _winReward.items.Add(new RewardItemModel(RewardItemType.Gem, 3));
                _loseReward.items.Add(new RewardItemModel(RewardItemType.Coin, 5 ));
                break;
            case 4:
                // plain + forest
                // action card + move card
                _map = new MapModel(5, 6, 16, 10, 2, 5, 4, 0, 1);
                List<CardModel> playerDeck4 = new List<CardModel>();
                playerDeck4.Add(new CardModel("SwordWater",new DamageCardAction(5,3),ConditionType.EnemyOnTileTypeWater));
                playerDeck4.Add(new CardModel("Horse",new MoveEffectCardAction(MoveType.HorseTrainer,3)));
                List<CardModel> enemyDeck4 = new List<CardModel>();
                enemyDeck4.Add(new CardModel("SwordForest",new DamageCardAction(5,3),ConditionType.EnemyOnTileTypeForest));
                enemyDeck4.Add(new CardModel("Horse",new MoveEffectCardAction(MoveType.HorseTrainer,3)));
                _players.Add(new PlayerModel(true, GetTilePerCoordinates(0,0),playerDeck4,1,GameController._gC.gamer));
                _players.Add(new PlayerModel(false, GetTilePerCoordinates(_map._columns - 1,_map._rows - 1 ),enemyDeck4,1));
                _tutorialMessageModels.Add(new TutorialMessageModel(0,"You can play one card per turn",ScreenLocation.Bottom, AdditionalElement.arrow, AdditionalElementPlace.playerCard));
                _tutorialMessageModels.Add(new TutorialMessageModel(2,"Watch out the <color=#00FF00>forest</color> tiles!",ScreenLocation.Top));
                _winReward.items.Add(new RewardItemModel(RewardItemType.Coin, 30));
                _winReward.items.Add(new RewardItemModel(RewardItemType.Gem, 3));
                _loseReward.items.Add(new RewardItemModel(RewardItemType.Coin, 5 ));
                break;
            default:
                _map = new MapModel(5, 6, 16, 10, 2, 5, 4, 0, 1);
                List<CardModel> playerDeckD = new List<CardModel>();
                playerDeckD.Add(new CardModel("SwordWater",new DamageCardAction(5,3),ConditionType.EnemyOnTileTypeWater, ConditionType.Any,false,0,3));
                playerDeckD.Add(new CardModel("Horse",new MoveEffectCardAction(MoveType.HorseTrainer,3),ConditionType.Any,ConditionType.Any,false,0,3));
                List<CardModel> enemyDeckD = new List<CardModel>();
                enemyDeckD.Add(new CardModel("SwordForest",new DamageCardAction(5,3),ConditionType.EnemyOnTileTypeForest));
                enemyDeckD.Add(new CardModel("Horse",new MoveEffectCardAction(MoveType.HorseTrainer,3)));
                _players.Add(new PlayerModel(true, GetTilePerCoordinates(0,0),playerDeckD,1,GameController._gC.gamer));
                _players.Add(new PlayerModel(false, GetTilePerCoordinates(_map._columns - 1,_map._rows - 1 ),enemyDeckD,1));
                _winReward.items.Add(new RewardItemModel(RewardItemType.Coin, 30));
                _winReward.items.Add(new RewardItemModel(RewardItemType.Gem, 3));
                _loseReward.items.Add(new RewardItemModel(RewardItemType.Coin, 5 ));
                break;
        }
        
        DefineActivePlayer();
        GetActivePlayer().avatar.SetAvatarMoves(_players,_map._tiles);
    }

    public void RewardPlayer(bool win)
    {
        if (win)
        {
            GameController._gC.gamer.coins += _winReward.GetCoins();
            GameController._gC.gamer.gems += _winReward.GetGems();    
        }
        else
        {
            GameController._gC.gamer.coins += _loseReward.GetCoins();
            GameController._gC.gamer.gems += _loseReward.GetGems();  
        }
    }

    public TileModel GetTilePerCoordinates(int x, int y)
    {
        //Debug.Log(_map);
        
        foreach (TileModel tile in _map._tiles)
        {
            if (tile.x == x && tile.y == y)
            {
                return tile;
            }
        }
        return _map._tiles[1];
    }

    public PlayerModel GetActivePlayer()
    {
        foreach (PlayerModel player in _players)
        {
            if (player.isActive)
            {
                return player;
            }
        }

        return _players[0];
    }

    public void DefineActivePlayer()
    {
        if (_turnNumber == 0)
        {
            int rand = UnityEngine.Random.Range(0, 1);
            Debug.Log(rand);
            _players[rand].isActive = true;
        }
        else
        {
            
            if (_players.Count > 1)
            {
                
                if (GetActivePlayer().isPlayer)
                {
                    
                    foreach (PlayerModel player in _players)
                    {
                        if (player.isPlayer)
                        {
                            
                            player.isActive = false;
                            
                        }
                        else
                        {
                            player.isActive = true;
                        }
                    }
                }
                else
                {
                    foreach (PlayerModel player in _players)
                    {
                        if (player.isPlayer)
                        {
                            player.isActive = true;
                        }
                        else
                        {
                            player.isActive = false;
                        }
                    }
                }
            }
        }
        
        //ResetAvatar???
        GetActivePlayer().avatar.moveType = MoveType.SingleMove;
        GetActivePlayer().RefreshDeck();
    }
    
    public PlayerModel OppositePlayer()
    {
        foreach (PlayerModel player in _players)
        {
            if (player != GetActivePlayer())
            {
                return player;
            }
        }

        return null;
    }

    public PlayerModel GetPlayer()
    {
        foreach (PlayerModel player in _players)
        {
            if (player.isPlayer == true)
            {
                return player;
            }
        }

        return null;
    }

    public PlayerModel GetPlayer(bool isPlayer)
    {
        foreach (PlayerModel player in _players)
        {
            if (player.isPlayer == isPlayer)
            {
                return player;
            }
        }
        return null;
    }

    public PlayerModel GetWinner()
    {
        
        if (_players.Count == 1)
        {
            if (GetActivePlayer().avatar.position.tileType == TileType.RedTower)
            {
                return GetActivePlayer();
            }
        }
        else
        {
            PlayerModel player = GetPlayer(true);
            PlayerModel enemy = GetPlayer(false);

            if (player.avatar.currentHP <= 0)
            {
                return enemy;
            }

            if (enemy.avatar.currentHP <= 0)
            {
                return player;
            }
            
            if (enemy.avatar.position.tileType == TileType.BlueTower)
            {
                return enemy;
            }
            
            if (player.avatar.position.tileType == TileType.RedTower)
            {
                return player;
            }
        }

        return null;
    }
    
    public void SelectCardForAI()
    {
        List<CardModel> availableCards = new List<CardModel>();

        PlayerModel ai = GetPlayer(false);
        
        foreach (CardModel card in ai.deck)
        {
            if (card.action.coolDownLeft == 0)
            {
                availableCards.Add(card);
            }
        }

        if (availableCards.Count > 0)
        {
            int rand = Random.Range(0, availableCards.Count);
            Debug.Log("Card is randomly selected");
            ai.activeCard = availableCards[rand];
        }
        else
        {
            Debug.Log("AI Card is null");
            ai.activeCard = null;
        }



    }
}
