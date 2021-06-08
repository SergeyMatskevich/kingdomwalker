using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardView : MovingObject
{
    public CardModel model;
    
    public Sprite[] attackActionSprites;
    public Sprite[] healActionSprites;
    public Sprite[] rookActionSprites;
    public Sprite[] bishopActionSprites;
    public Sprite[] horseActionSprites;
    public Sprite[] baseSprites;
    public Sprite locked;

    public GameObject baseCard;
    public GameObject actionCard;
    public GameObject top;
    public GameObject bottom;
    public GameObject cardState;
    public GameObject actionAmount;
    public GameObject coolDown;

    protected override void Start()
    {
        base.Start();
        GameController._gC.OnCardSelected += MakeCardSelected;
        GameController._gC.OnCardDeselected += MakeCardDeselected;
    }

    public void SetSprites()
    {
        if (model == null)
        {
            baseCard.GetComponent<SpriteRenderer>().sprite = locked;
            baseCard.GetComponent<SpriteRenderer>().color = new Color(1F,1F,1F,1F);
            actionCard.SetActive(false);
            top.SetActive(false);
            bottom.SetActive(false);
            actionAmount.SetActive(false);
            coolDown.SetActive(false);
            cardState.SetActive(false);
        }
        else
        {
            SetupActionSprite();
            SetupBaseCard();
            UpdateActionAmount();
            UpdateCooldown();
            cardState.SetActive(false);
        }
    }

    public void UpdateCooldown()
    {
        if (model.coolDown == 0)
        {
            coolDown.SetActive(false);
            bottom.SetActive(false);
        }
        else
        {
            coolDown.SetActive(true);
            coolDown.GetComponent<TextMeshPro>().text = model.coolDown.ToString();
            bottom.SetActive(true);
            bottom.GetComponent<SpriteRenderer>().color = new Color(0F,0.65F,0.69F,1F);
        }
    }

    public void UpdateActionAmount()
    {
        switch (model.action.ActionType)
        {
            case ActionType.DealDamage:
                actionAmount.GetComponent<TextMeshPro>().text = model.action.damage.ToString();
                top.SetActive(true);
                top.GetComponent<SpriteRenderer>().color = new Color(1F,0F,0F,1F);
                break;
            case ActionType.Heal:
                actionAmount.GetComponent<TextMeshPro>().text = model.action.heal.ToString();
                top.SetActive(true);
                top.GetComponent<SpriteRenderer>().color = new Color(1F,0F,0F,1F);
                break;
            case ActionType.TransformMove:
                actionAmount.GetComponent<TextMeshPro>().text = "";
                top.SetActive(false);
                break;
        }
        
    }

    public void SetupBaseCard()
    {
        switch (model.conditionForActionByPlayer)
        {
            case ConditionType.Any:
                baseCard.GetComponent<SpriteRenderer>().sprite = baseSprites[0];
                baseCard.GetComponent<SpriteRenderer>().color = new Color(0.176F,0.2235F,0.2039F,1F);
                break;
            case ConditionType.OwnerOnTileTypeForest:
                baseCard.GetComponent<SpriteRenderer>().sprite = baseSprites[1];
                baseCard.GetComponent<SpriteRenderer>().color = new Color(1F,1F,1F,1F);
                break;
            case ConditionType.OwnerOnTileTypeWater:
                baseCard.GetComponent<SpriteRenderer>().sprite = baseSprites[2];
                baseCard.GetComponent<SpriteRenderer>().color = new Color(1F,1F,1F,1F);
                break;
            case ConditionType.OwnerOnTileTypePlains:
                baseCard.GetComponent<SpriteRenderer>().sprite = baseSprites[3];
                baseCard.GetComponent<SpriteRenderer>().color = new Color(1F,1F,1F,1F);
                break;
        }
    }

    public void SetupActionSprite()
    {
        if (model.action.ActionType == ActionType.Heal)
        {
            DefineActionEnemyCondition(healActionSprites);
        }

        if (model.action.ActionType == ActionType.DealDamage)
        {
            DefineActionEnemyCondition(attackActionSprites);
        }
        
        Debug.Log(model.action.moveType);
        
        if (model.action.moveType == MoveType.Bishop)
        {
            DefineActionEnemyCondition(bishopActionSprites);
        }
        
        if (model.action.moveType == MoveType.CabCarl)
        {
            DefineActionEnemyCondition(rookActionSprites);
        }
        
        if (model.action.moveType == MoveType.HorseTrainer)
        {
            DefineActionEnemyCondition(horseActionSprites);
        }
    }

    public void DefineActionEnemyCondition(Sprite[] sourceSprites)
    {
        switch (model.conditionForActionByEnemy)
        {
            case ConditionType.Any:
                actionCard.GetComponent<SpriteRenderer>().sprite = sourceSprites[0];
                break;
            case ConditionType.EnemyOnTileTypeForest:
                actionCard.GetComponent<SpriteRenderer>().sprite = sourceSprites[1];
                break;
            case ConditionType.EnemyOnTileTypeWater:
                actionCard.GetComponent<SpriteRenderer>().sprite = sourceSprites[2];
                break;
            case ConditionType.EnemyOnTileTypePlains:
                actionCard.GetComponent<SpriteRenderer>().sprite = sourceSprites[3];
                break;
        }
    }

    public void DefineAndSetSprite(GameObject objToSet, List<Sprite> source)
    {
        
    }

    public void OnMouseDown()
    {
        Debug.Log("Card pressed");
        GameController._gC.SelectCard(this);
    }

    public void MakeCardSelected()
    {
        if (model == GameController._gC._game.GetActivePlayer().activeCard)
        {
            cardState.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
        }
    }

    public void MakeCardDeselected()
    {
        cardState.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
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

}
