using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardView : MovingObject
{
    public CardModel model;

    public GameObject action;
    public Animator actionAnimator;

    public Sprite[] sprites;
    public Sprite[] actionSprites;
    public Animator animator;


    public Vector3 ordinaryPlace;
    public Vector3 startPlace;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSprites()
    {
        switch (model.name)
        {
            case "Bishop":
                GetComponent<SpriteRenderer>().sprite = sprites[0];
                action.GetComponent<SpriteRenderer>().sprite = actionSprites[0];
                break;
            case "Cab Carl":
                GetComponent<SpriteRenderer>().sprite = sprites[1];
                action.GetComponent<SpriteRenderer>().sprite = actionSprites[1];
                break;
            case "Horse Trainer":
                GetComponent<SpriteRenderer>().sprite = sprites[2];
                action.GetComponent<SpriteRenderer>().sprite = actionSprites[2];
                break;
            case "Healer Tom":
                GetComponent<SpriteRenderer>().sprite = sprites[3];
                action.GetComponent<SpriteRenderer>().sprite = actionSprites[3];
                break;
            case "Fisherman":
                GetComponent<SpriteRenderer>().sprite = sprites[4];
                action.GetComponent<SpriteRenderer>().sprite = actionSprites[4];
                break;
            case "Log Thrower":
                GetComponent<SpriteRenderer>().sprite = sprites[5];
                action.GetComponent<SpriteRenderer>().sprite = actionSprites[5];
                break;
            case "Goatherd":
                GetComponent<SpriteRenderer>().sprite = sprites[6];
                action.GetComponent<SpriteRenderer>().sprite = actionSprites[6];
                break;
        }
    }

    public Vector3 DeckCoordToUICoord(Vector3 coord, PlayerModel player)
    {
        float xAdj = 4.5F ;
        float yAdj = 9F ;

        Vector3 result;

        if (player.isPlayer)
        {
            result = new Vector3(coord.x - xAdj, coord.y - yAdj);
        }
        else
        {
            result = new Vector3(coord.x - xAdj, coord.y + yAdj);
        }

        return result;
    }
    public void MoveToPlace(PlayerModel player)
    {
        UpdateAnimation();
        GetComponent<DoTweenController>().MoveToPosition(DeckCoordToUICoord(startPlace, player));
        //Move(DeckCoordToUICoord(startPlace,player));
        //action.GetComponent<Animator>().SetTrigger("BackToDeck");
    }

    public void MoveToOrdinaryPlace(PlayerModel player)
    {
        UpdateAnimation();
        GetComponent<DoTweenController>().MoveToPosition(DeckCoordToUICoord(ordinaryPlace, player));
        //Move(DeckCoordToUICoord(ordinaryPlace,player));
    }

    public void SuccessAction()
    {
        actionAnimator.SetTrigger("Success");
    }

    public void FailAction()
    {
        actionAnimator.SetTrigger("Fail");
    }

    public void ActionIdle()
    {
        actionAnimator.SetTrigger("BackToDeck");
    }

    public void UpdateAnimation()
    {
        animator.SetBool("CardOnTable", model.onTable);
    }

    
}
