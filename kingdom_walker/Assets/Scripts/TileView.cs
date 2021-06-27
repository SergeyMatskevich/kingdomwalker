using System;
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
        GameController._gC.OnTileAvailableForMove += UpdateAnimation;
    }

    private void OnDestroy()
    {
        GameController._gC.OnTileAvailableForMove -= UpdateAnimation;
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

        UpdateAnimation();
    }

    // Goal is to set here all the animations
    public void UpdateAnimation()
    {
        //Debug.Log(model.availableForMove);
        animator.SetBool("AvailableForMove", model.availableForMove);   
    }

    public void OnMouseDown()
    {
        if (model.availableForMove)
        {
            GameController._gC.ClearTiles();
            GameController._gC.AvatarMove(model);
            GameController._gC.ResetAvatar();
            //GameController._gC.SetNextTurn();

        }
    }

    public void MoveCardOnUIBack()
    {
        //GameController._gC._cC.MoveCardOnUIBack(GameController._gC._game.player);
    }
}
