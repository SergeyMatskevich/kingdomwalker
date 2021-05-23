using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController _gC;
    public MapController _mC;
    public CardController _cC;
    public PlayerController _pC;
    public UIController _UIC;
    public GameModel _game;
    public FirebaseController _fC;
    public AdsController _aC;

    public bool playerTurn = false;
    public int turnNumber = 0;
    
    public int level;
    
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
    
    public void InitLevel()
    {
        //Debug.Log(SystemInfo.deviceUniqueIdentifier);
        _UIC.HideUI();
        turnNumber = 0;
        _game = new GameModel(level);
        _mC.InitMapObjects(_game.map);
        _pC.InitAvatars(_game);
        _cC.InitCardsObjects(level, _game.player);
        _cC.InitCardsObjects(level, _game.enemy);
        _cC.SetCardsInDeck(_game.player);
        _cC.SetCardsInDeck(_game.enemy);
        _cC.PopulateDeckOnUI(_game.player);
        _cC.PopulateDeckOnUI(_game.enemy);

        DefineTurn();
        SetNextTurn();

        string id = SystemInfo.deviceUniqueIdentifier;
        string time = System.DateTime.Now.ToString();

        _fC.SaveStatistics(id,time,level.ToString(),"lvlstart",turnNumber.ToString(), Time.time.ToString());
    }
    
    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + " seconds");
        string id = SystemInfo.deviceUniqueIdentifier;
        string time = System.DateTime.Now.ToString();

        _fC.SaveStatistics(id, time, level.ToString(), "gamequit", turnNumber.ToString(), Time.time.ToString());
    }
    
    public void DefineTurn()
    {
        int rand = UnityEngine.Random.Range(0, 2);

        //Debug.Log(rand);
        if (rand == 1)
        {
            playerTurn = true;
        }
        else
        {
            playerTurn = false;
        }
    }

    public void SetNextTurn()
    {
        bool go = CheckGameStatus();

        if (go)
        {
            turnNumber += 1;

            if (playerTurn)
            {
                //Debug.Log("PlayerTurn");
                if (turnNumber > 1)
                {
                    _cC.SelectPlayerCard(_game.player);
                    _cC.MoveCardOnUI(_game.player);

                    Invoke("InvokePlayerCardAnimation", 1.2F);
                    //AnimateCardAction();
                    Invoke("InvokePlayerMoveAnimation", 2.4F);
                    //_mC.SetPlayerMoves(_game.player, _game.map, _game.enemy);
                    //WaitingPlayerInput.
                }
                else
                {
                    Invoke("InvokePlayerMoveAnimation", 0F);
                    _mC.SetPlayerMoves(_game.player, _game.map, _game.enemy);
                    //WaitingPlayerInput.
                }

            }
            else
            {
                if (turnNumber > 1)
                {
                    _cC.SelectPlayerCard(_game.enemy);
                    _cC.MoveCardOnUI(_game.enemy);

                    Invoke("InvokeEnemyCardAnimation", 1.2F);
                    Invoke("InvokeEnemyMoveAnimation", 2.4F);
                    Invoke("MoveCardOnUIBack", 3.6F);
                    playerTurn = true;

                    Invoke("SetNextTurn", 4.8F);
                }
                else
                {
                    Invoke("InvokeEnemyMoveAnimation", 0F);
                    playerTurn = true;
                    Invoke("SetNextTurn", 1.2F);
                }
            }
        }
    }
    public void InvokePlayerCardAnimation()
    {
        _cC.InvokeCard(_game.player, _game.enemy);
        //cC.AnimateCardAction(game.player, game.enemy);
    }

    public void InvokeEnemyCardAnimation()
    {
        _cC.InvokeCard(_game.enemy, _game.player);
        //cC.AnimateCardAction(game.enemy, game.player);
    }

    public void InvokePlayerMoveAnimation()
    {
        _mC.SetPlayerMoves(_game.player, _game.map, _game.enemy);
    }

    public void InvokeEnemyMoveAnimation()
    {
        _mC.SetPlayerMoves(_game.enemy, _game.map, _game.player);
        _pC.AvatarMove(_game.enemy, _mC.SetTileForAI(_game.map, _game.enemy));
        _pC.ResetAvatar(_game.enemy);
        _mC.ClearTiles(_game.map);
    }

    public void MoveCardOnUIBack()
    {
        _cC.MoveCardOnUIBack(_game.enemy);
    }

    public bool CheckGameStatus()
    {
        if (_game.player.avatar.currentHP <= 0 && _game.enemy.avatar.currentHP <= 0)
        {
            Debug.Log("TIE");
            
            return false;
        }
        else if (_game.player.avatar.currentHP <= 0 && _game.enemy.avatar.currentHP > 0)
        {
            Debug.Log("ENEMY WON");
            _UIC.SetResultMessage("YOU LOSE, DEAL WITH IT", "HE'S TOUGH");
            CloseLevel(false, level);
            return false;
        }
        else if (_game.player.avatar.currentHP > 0 && _game.enemy.avatar.currentHP <= 0)
        {
            Debug.Log("YOU WIN");
            _UIC.SetResultMessage("YOU WIN", "DON'T SHOW OFF");
            CloseLevel(true, level);
            return false;
        }
        else if (_game.player.avatar.position.tileType == TileType.RedTower &&
            _game.enemy.avatar.position.tileType == TileType.BlueTower)
        {
            Debug.Log("TIE");
            return false;
        }
        else if (_game.enemy.avatar.position.tileType == TileType.BlueTower)
        {
            Debug.Log("ENEMY WON");
            _UIC.SetResultMessage("YOU LOSE", "HE'S FAST AS LIGHTNING!");
            CloseLevel(false, level);
            return false;
        }
        else if (_game.player.avatar.position.tileType == TileType.RedTower)
        {
            Debug.Log("YOU WIN");
            _UIC.SetResultMessage("MMM? YOU WIN...", "FIGHT, COWARD! STOP RUNNING!");
            CloseLevel(true, level);
            return false;
        }
        else
        {
            return true;
        }
    }

    public void CloseLevel(bool win, int lvl)
    {
        //
        _UIC.ShowResultScreen(win, lvl);
        Destroy(GameObject.Find("Map"));
        Destroy(GameObject.Find("DeckPlayer"));
        Destroy(GameObject.Find("DeckEnemy"));
        _pC.avatarEnemy = null;
        _pC.avatarPlayer = null;
        _game = null;
        _cC.playerDeck.Clear();
        _cC.enemyDeck.Clear();
        _mC.UITiles.Clear();
        
        string id = SystemInfo.deviceUniqueIdentifier;
        string time = System.DateTime.Now.ToString();

        _fC.SaveStatistics(id, time, level.ToString(), "closelevel", turnNumber.ToString(), Time.time.ToString());

    }
}
