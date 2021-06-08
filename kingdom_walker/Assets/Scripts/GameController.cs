using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public static GameController _gC;
    public FieldController _fieldC;
    public MapController _mC;
    public CardController _cC;
    public PlayerController _pC;
    public UIController _UIC;
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
    public UnityAction OnCardDeselected;
    
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
        InitLevel();
    }
    
    public void StartNewLevel()
    {
        //_aC.ShowInterstitial();
        level += 1;
        InitLevel();
    }
    
    public void InitLevel()
    {
        _rC.rewardPanel.SetActive(false);
        
        _game = new GameModel(level);
        _fieldC.InitGameField(_game);
        
        string id = SystemInfo.deviceUniqueIdentifier;
        string time = System.DateTime.Now.ToString();

        _fC.SaveStatistics(id,time,level.ToString(),"lvlstart",_game._turnNumber.ToString(), Time.time.ToString());
    }
    
    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + " seconds");
        string id = SystemInfo.deviceUniqueIdentifier;
        string time = System.DateTime.Now.ToString();

        _fC.SaveStatistics(id, time, level.ToString(), "gamequit", _game._turnNumber.ToString(), Time.time.ToString());
    }
    

    public void SetActiveAvatarMoves()
    {
        ClearTiles();
        
        _game.GetActivePlayer().avatar.SetAvatarMoves(_game._players, _game._map._tiles);

        OnTileAvailableForMove?.Invoke();
    }


    public void SetNextTurn()
    {
        
        if (CheckGameStatus())
        {
            _game._turnNumber += 1;
            
            //Debug.Log(_game.GetActivePlayer().isPlayer);
            _game.DefineActivePlayer();
            //Debug.Log(_game.GetActivePlayer().isPlayer);
            
            if (_game.GetActivePlayer().isPlayer)
            {
                _gEC.InstantiateMessage();
                SetActiveAvatarMoves();
            }
            else
            {
                _game.GetActivePlayer().SelectRandomCard();
                InvokeCard();
                Invoke("InvokeEnemyMoveAnimation", 0.5F);
                Invoke("SetNextTurn", 1F);
                
            }
        }
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

    public void SelectCard(CardView view)
    {
        PlayerModel target = _game.OppositePlayer();

        if (view.model.CardFromPlayerDeck(_game.GetActivePlayer()) && _game.GetActivePlayer().activeCard == null)
        {
            _game.GetActivePlayer().activeCard = view.model;
            OnCardSelected?.Invoke();
            //TODO: refactor on card that should be made made unavailable;  
            
            if (view.model.CheckCondition(_game.GetActivePlayer(), target))
            {
                Debug.Log("Condition checked");
                view.model.action.Invoke(_game.GetActivePlayer(), target);
                if (view.model.action.ActionType == ActionType.DealDamage)
                {
                    Instantiate(attackAnimation, GetAvatarByModel(target).transform);
                }
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

    public void ClearTiles()
    {
        _game._map.ClearTilesForMove();
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

    public void InvokePlayerCardAnimation()
    {
        //_cC.InvokeCard(_game.player, _game.enemy);
        //cC.AnimateCardAction(game.player, game.enemy);
    }

    public void InvokeEnemyCardAnimation()
    {
        //_cC.InvokeCard(_game.enemy, _game.player);
        //cC.AnimateCardAction(game.enemy, game.player);
    }

    public void InvokePlayerMoveAnimation()
    {
        //_mC.SetPlayerMoves();
    }

    public void InvokeEnemyMoveAnimation()
    {
        SetActiveAvatarMoves();
        
        _fieldC.AvatarMove(_mC.SetTileForAI(_game._map, _game.GetActivePlayer()));
        
        ResetAvatar();
        ClearTiles();
    }

    public void MoveCardOnUIBack()
    {
        //_cC.MoveCardOnUIBack(_game.enemy);
    }

    public PlayerModel GetPlayer(bool isPlayer)
    {
        foreach (PlayerModel player in _game._players)
        {
            if (player.isPlayer == isPlayer)
            {
                return player;
            }
        }
        return null;
    }
    
    public void ResetAvatar()
    {
        _game.GetActivePlayer().avatar.moveType = MoveType.SingleMove;
        //player.activeCard.onTable = false;
        _game.GetActivePlayer().activeCard = null;
        
        OnCardDeselected?.Invoke();
    }

    // need to refactor it somehow
    public bool CheckGameStatus()
    {
        
        if (_game._players.Count == 1)
        {
            if (_game.GetActivePlayer().avatar.position.tileType == TileType.RedTower)
            {
                Debug.Log("YOU WIN 1");
                _gEC.DestroyCurrentMessage();
                //_gEC.InstantiateWinMessage();
                _rC.SetupWinRewards();
                //_UIC.SetResultMessage("YOU WIN", "DON'T SHOW OFF");
                //CloseLevel(true, level);
                return false;
            }
        }
        else
        {
            PlayerModel player = GetPlayer(true);
            PlayerModel enemy = GetPlayer(false);
        
            if (player.avatar.currentHP <= 0 && enemy.avatar.currentHP <= 0)
            {
                Debug.Log("TIE");
            
                return false;
            }
            else if (player.avatar.currentHP <= 0 && enemy.avatar.currentHP > 0)
            {
                Debug.Log("ENEMY WON");
                //_UIC.SetResultMessage("YOU LOSE, DEAL WITH IT", "HE'S TOUGH");
                //CloseLevel(false, level);
                return false;
            }
            else if (player.avatar.currentHP > 0 && enemy.avatar.currentHP <= 0)
            {
                Debug.Log("YOU WIN");
                _gEC.DestroyCurrentMessage();
                //_gEC.InstantiateWinMessage();
                _rC.SetupWinRewards();
                //_UIC.SetResultMessage("YOU WIN", "DON'T SHOW OFF");
                //CloseLevel(true, level);
                return false;
            }
            else if (player.avatar.position.tileType == TileType.RedTower &&
                     enemy.avatar.position.tileType == TileType.BlueTower)
            {
                Debug.Log("TIE");
                return false;
            }
            else if (enemy.avatar.position.tileType == TileType.BlueTower)
            {
                Debug.Log("ENEMY WON");
                
                return false;
            }
            else if (player.avatar.position.tileType == TileType.RedTower)
            {
                Debug.Log("YOU WIN");
                _gEC.DestroyCurrentMessage();
                //_gEC.InstantiateWinMessage();
                _rC.SetupWinRewards();
                return false;
            }
            else
            {
                return true;
            }
        }

        return true;
    }
}
