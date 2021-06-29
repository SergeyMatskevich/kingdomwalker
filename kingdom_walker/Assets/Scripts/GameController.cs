using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Firebase.Analytics;

public class GameController : MonoBehaviour
{
    public static GameController _gC;
    public FieldController _fieldC;
    public MapController _mC;
    public CardController _cC;
    public PlayerController _pC;
    public GameModel _game;
    public FirebaseController _fC;
    public AdsController _aC;
    public GameEventController _gEC;
    public RewardController _rC;

    public GameObject attackAnimation;

    public int level;
    
    public UnityAction OnAvatarChangeHP;
    public UnityAction OnTileAvailableForMove;
    public UnityAction OnCardSelected;
    public UnityAction<TileModel,PlayerModel> OnAvatarPositionChanged;
    public UnityAction<CardModel> OnCardInCooldown;
    public UnityAction<CardModel> OnCardConditionFalse;
    public UnityAction<CardModel> OnCardOutCooldown;
    public UnityAction OnResourceUpdate;
    public UnityAction<CardModel> OnCardRefreshed;

    public GamerModel gamer;
    
    private void Awake()
    {
        if (_gC == null)
        {
            DontDestroyOnLoad(gameObject);
            _gC = this;
        }
        else if (_gC != this)
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        gamer = Load();
        InitLevel();
    }
    
    public void StartNewLevel()
    {
        //_aC.ShowInterstitial();
        Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLevelEnd, 
            new Parameter(FirebaseAnalytics.ParameterLevel, gamer.level));
        gamer.level += 1;
        _game.RewardPlayer(true);
        OnResourceUpdate?.Invoke();
        Save();
        InitLevel();
    }

    public void RewardLose()
    {
        _game.RewardPlayer(false);
        OnResourceUpdate?.Invoke();
        Save();
    }

    public void InitLevel()
    {
        _rC.rewardPanel.SetActive(false);
        _game = new GameModel(gamer.level);
        _fieldC.InitGameField(_game);

        Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLevelStart, 
            new Parameter(FirebaseAnalytics.ParameterLevel, gamer.level));
        
    }
    
    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + " seconds");
        string id = SystemInfo.deviceUniqueIdentifier;
        string time = System.DateTime.Now.ToString();
        
    }
    
    public void SetActiveAvatarMoves()
    {
        ClearTiles();
        
        _game.GetActivePlayer().avatar.SetAvatarMoves(_game._players, _game._map._tiles);

        OnTileAvailableForMove?.Invoke();
    }

    public void DefineRewardPanel()
    {
        if (_game.GetWinner().isPlayer == true)
        {
            _gEC.DestroyCurrentMessage();
            _rC.SetupWinRewards();
        }
        else
        {
            _gEC.DestroyCurrentMessage();
            _rC.SetupDefeatRewards();
        }
    }

    public void CheckWinnerMove()
    {
        if (_game.GetWinner() == null)
        {
            SetNextTurn();
        }
        else
        {
            Debug.Log("GameOver");
            DefineRewardPanel();
        }
    }

    public void CheckWinnerCard()
    {
        if (_game.GetWinner() != null)
        {
            Debug.Log("GameOver");
            DefineRewardPanel();
        }
    }

    public void SetNextTurn()
    {
        _game._turnNumber += 1;
        
        _game.DefineActivePlayer();
        
        Debug.Log("Turn number: " + _game._turnNumber);

        if (_game.GetActivePlayer().isPlayer)
        {
            _gEC.InstantiateMessage();
            SetActiveAvatarMoves();
        }
        else
        {
            Debug.Log("Enemy goes");
            StartCoroutine(ComposeEnemyMove());
        }
        
    }

    IEnumerator ComposeEnemyMove()
    {
        Debug.Log("Active player is player" + _game.GetActivePlayer().isPlayer);
        PlayerModel target = _game.OppositePlayer();
        PlayerModel ai = _game.GetActivePlayer();
        
        _game.SelectCardForAI();
        if (ai.activeCard != null)
        {
            if (ai.activeCard.CheckCondition(ai, target))
            {
                ai.activeCard.InvokeAction(ai, target);
                OnCardSelected?.Invoke();
                if (ai.activeCard.action.ActionType == ActionType.DealDamage)
                {
                    Instantiate(attackAnimation, GetAvatarByModel(target).transform);
                }
                OnAvatarChangeHP?.Invoke();
            }

            yield return new WaitForSeconds(1F);
        }
        
        SetActiveAvatarMoves();
        
        AvatarMove(_mC.SetTileForAI(_game._map, _game.GetActivePlayer()));

        ClearTiles();
        
        // define whether to play card
        // if so -> define what card to play
        // compose sequence of actions : card invoke on ui -> select where to make move -> 
    }
    
    public void SelectCard(CardView view)
    {
        PlayerModel target = _game.OppositePlayer();
        
        if (view.model.CardFromPlayerDeck(_game.GetActivePlayer()) 
            && _game.GetActivePlayer().activeCard == null 
            && _game.GetActivePlayer().isPlayer == true)
        {
            //TODO: refactor on card that should be made made unavailable;  

            if (view.model.CardIsInCooldown() == false)
            {
                if (view.model.CheckCondition(_game.GetActivePlayer(), target))
                {
                    Debug.Log("Condition checked");
                    view.model.InvokeAction(_game.GetActivePlayer(), target);
                    OnCardSelected?.Invoke();
                    if (view.model.action.ActionType == ActionType.DealDamage)
                    {
                        Instantiate(attackAnimation, GetAvatarByModel(target).transform);
                    }
                }
                else
                {
                    OnCardConditionFalse?.Invoke(view.model);
                }
            }
            else
            {
                Debug.Log("In cooldown");
                OnCardInCooldown?.Invoke(view.model);
            }

            
        }
        else
        {
            Debug.Log("Not your card or card is already selected");
        }

        SetActiveAvatarMoves();
        
        OnTileAvailableForMove?.Invoke();
        OnAvatarChangeHP?.Invoke();
   
    }
    
    public void InvokeCard()
    {
        PlayerModel target = _game.OppositePlayer();

        if (_game.GetActivePlayer().activeCard != null && _game.GetActivePlayer().activeCard.CheckCondition(_game.GetActivePlayer(), target))
        {
            _game.GetActivePlayer().activeCard.action.Invoke(_game.GetActivePlayer(), target);
            
            
            //_activePlayer.activeCard.onTable = false;
        }
        //AnimateCardAction(player, enemy);
    }

    public GameObject GetAvatarByModel(PlayerModel model)
    {
        foreach (GameObject avatar in _fieldC.avatars)
        {
            if (avatar.GetComponent<AvatarView>().model == model)
            {
                return avatar;
            }
        }

        return null;
    }

    public void UnfreezeCard(CardView view)
    {
        if (view.model.cooldownRemoveCost <= gamer.gems)
        {
            view.model.Unfreeze();
            gamer.gems -= view.model.cooldownRemoveCost;
            OnCardOutCooldown?.Invoke(view.model);
            OnResourceUpdate?.Invoke();
            
            FirebaseAnalytics.LogEvent("UnfreezeCard", 
                    new Parameter("Success","true"), 
                                    new Parameter("Cost", view.model.cooldownRemoveCost),
                                    new Parameter(FirebaseAnalytics.ParameterLevel,gamer.level));
        }
        else
        {
            Debug.Log("Not enough gems");
            
            FirebaseAnalytics.LogEvent("UnfreezeCard", 
                    new Parameter("Success","false"), 
                                new Parameter("Cost", view.model.cooldownRemoveCost),
                                new Parameter(FirebaseAnalytics.ParameterLevel,gamer.level));
        }

        //check the player balance is enough
        //update card cooldown
        //refresh card back
    }


    

    public void ClearTiles()
    {
        _game._map.ClearTilesForMove();
    }

    public void ResetAvatar()
    {
        //_game.GetActivePlayer().avatar.moveType = MoveType.SingleMove;
        //_game.GetActivePlayer().RefreshDeck();
    }
    
    public void AvatarMove(TileModel tile)
    {
        PlayerModel player = _game.GetActivePlayer(); 
        
        player.avatar.MoveAvatar(tile);
        
        OnAvatarPositionChanged?.Invoke(tile,player);
    }
    
    public GameObject GetTileUI(TileModel tile)
    {
        foreach (GameObject tileUI in _fieldC.tiles)
        {
            if (tileUI.GetComponent<TileView>().model == tile)
            {
                return tileUI;
            }
        }

        return null;
    }
    
    public void Save()
    {
        if (File.Exists(Application.persistentDataPath + "/gamerData.dat"))
        {
            File.Delete(Application.persistentDataPath + "/gamerData.dat");
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamerData.dat");

        GamerData data = new GamerData
        {
            coinBalance = gamer.coins, 
            gemBalance = gamer.gems,
            level = gamer.level
            
        };

        bf.Serialize(file, data);
        Debug.Log("Player Data is saved");
        file.Close();

    }
    
    public GamerModel Load()
    {
        Debug.Log(Application.persistentDataPath);
        if (File.Exists(Application.persistentDataPath + "/gamerData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamerData.dat", FileMode.Open);
            GamerData data = (GamerData)bf.Deserialize(file);
            
            GamerModel model = new GamerModel(data.coinBalance, data.gemBalance, data.level);
            
            file.Close();
            Debug.Log("Player data is loaded!");    
            return model;
            
        }else
        {
            Debug.Log("No player data to load!");
            return new GamerModel();
            
        }

    }
    
    [Serializable]
    public class GamerData
    {
        public int coinBalance;
        public int gemBalance;
        public int level;
    }
}
