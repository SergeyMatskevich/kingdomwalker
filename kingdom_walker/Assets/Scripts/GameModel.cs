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
                _players.Add(new PlayerModel(true, GetTilePerCoordinates(0,0)));
                _tutorialMessageModels.Add(new TutorialMessageModel(0,"TAP SHAKING TILE TO MOVE!",ScreenLocation.Bottom, AdditionalElement.tap,AdditionalElementPlace.moveTile));
                _tutorialMessageModels.Add(new TutorialMessageModel(1,"GOOD! YOU NEED TO REACH RED CASTLE TO GET REWARD!",ScreenLocation.Top, AdditionalElement.arrow,AdditionalElementPlace.redCastleTile));
                _winReward.items.Add(new RewardItemModel(RewardItemType.Silver, 100));
                _winReward.items.Add(new RewardItemModel(RewardItemType.Gold, 10));
                break;
            case 2:
                _map = new MapModel(4, 3, 0, 0, 10, 2, 2, 0, 0);
                List<CardModel> playerDeck2 = new List<CardModel>();
                playerDeck2.Add(new CardModel("Sword",new DamageCardAction(5),ConditionType.Any,CoolDown:0,ConditionType.Any));
                List<CardModel> enemyDeck2  = new List<CardModel>();
                enemyDeck2.Add(new CardModel("PlainsRook",new MoveEffectCardAction(MoveType.CabCarl),ConditionType.Any));
                _players.Add(new PlayerModel(true, GetTilePerCoordinates(0,0),playerDeck2,2));
                _players.Add(new PlayerModel(false, GetTilePerCoordinates(_map._columns - 1,_map._rows - 1 ),enemyDeck2,1));
                _tutorialMessageModels.Add(new TutorialMessageModel(0,"YOU ARE NOT ALONE! ENEMY WANTS TO CAPTURE YOUR CASTLE!",ScreenLocation.Bottom, AdditionalElement.arrow, AdditionalElementPlace.enemy));
                _tutorialMessageModels.Add(new TutorialMessageModel(2,"TRY TO PLAY CARDS BEFORE MAKE MOVE",ScreenLocation.Top, AdditionalElement.tap, AdditionalElementPlace.playerCard));
                _winReward.items.Add(new RewardItemModel(RewardItemType.Silver, 100));
                _winReward.items.Add(new RewardItemModel(RewardItemType.Gold, 10));
                break;
            default:
                _map = new MapModel(5, 5, 7, 8, 9, 4, 4, 0, 0);
                _players.Add(new PlayerModel(true, GetTilePerCoordinates(0,0)));
                _players.Add(new PlayerModel(false, GetTilePerCoordinates(4,4)));
                break;
        }
        
        DefineActivePlayer();
        GetActivePlayer().avatar.SetAvatarMoves(_players,_map._tiles);
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
            //Debug.Log("TurnNumber > 0");
            if (_players.Count > 1)
            {
                //Debug.Log("Players Count > 1");
                if (GetActivePlayer().isPlayer)
                {
                    //Debug.Log("Player is active player before");
                    foreach (PlayerModel player in _players)
                    {
                        if (player.isPlayer)
                        {
                            //Debug.Log("Found Player in players.");
                            player.isActive = false;
                            //Debug.Log("Make Player in players inactive");
                        }
                        else
                        {
                            player.isActive = true;
                        }
                    }
                }
                else
                {
                    //Debug.Log("Comp is active player");
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
}
