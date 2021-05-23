using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileView : MonoBehaviour
{
    public TileModel model;
    public Sprite[] sprites;
    public Animator animator;

    private void Start()
    {
        
    }

    public void SetSprites()
    {
        switch (model.tileType)
        {
            case TileType.BlueTower:
                GetComponent<SpriteRenderer>().sprite = sprites[0];
                break;
            case TileType.RedTower:
                GetComponent<SpriteRenderer>().sprite = sprites[1];
                break;
            case TileType.Forest:
                GetComponent<SpriteRenderer>().sprite = sprites[2];
                break;
            case TileType.Plain:
                GetComponent<SpriteRenderer>().sprite = sprites[3];
                break;
            case TileType.Water:
                GetComponent<SpriteRenderer>().sprite = sprites[4];
                break;
        }
    }

    // Goal is to set here all the animations
    public void UpdateAnimation()
    {
        animator.SetBool("AvailableForMove", model.availableForMove);
    }

    public void OnMouseDown()
    {
        if (model.availableForMove)
        {
            GameController._gC._mC.ClearTiles(GameController._gC._game.map);
            GameController._gC._pC.AvatarMove(GameController._gC._game.player, model);
            GameController._gC._pC.ResetAvatar(GameController._gC._game.player);
            if (GameController._gC.turnNumber > 1)
            {
                Invoke("MoveCardOnUIBack", 1.2F);
                Invoke("SetNextTurn", 2.4F);
            }
            else
            {
                Invoke("SetNextTurn", 1.2F);
            }
            
        }
    }

    public void MoveCardOnUIBack()
    {
        GameController._gC._cC.MoveCardOnUIBack(GameController._gC._game.player);
    }

    public void SetNextTurn()
    {
        GameController._gC.playerTurn = false;
        //GameController.gC.turnNumber += 1;
        GameController._gC.SetNextTurn();
    }
}
