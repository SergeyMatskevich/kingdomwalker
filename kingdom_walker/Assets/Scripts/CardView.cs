using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MovingObject
{
    public CardModel model;
    public Rigidbody2D rigidbody2D;
    public SpriteRenderer baseRenderer;
    public SpriteRenderer topRenderer;
    public SpriteRenderer bottomRenderer;
    public SpriteRenderer actionRenderer;

    public float duration = 1F;
    
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
    
    public GameObject buttonUpgrade;
    public TextMeshProUGUI upgradeCost;
    public GameObject buttonRefresh;
    public TextMeshProUGUI refreshCost;
    
    
    protected override void Start()
    {
        base.Start();
        GameController._gC.OnCardSelected += MakeCardSelected;
        GameController._gC.OnCardInCooldown += UpdateCooldownCheck;
        GameController._gC.OnCardOutCooldown += MoveOutCooldown;
        GameController._gC.OnCardConditionFalse += ActionFalseAnimation;
        GameController._gC.OnCardRefreshed += RefreshCard;
    }

    public void OnDestroy()
    {
        GameController._gC.OnCardSelected -= MakeCardSelected;
        GameController._gC.OnCardInCooldown -= UpdateCooldownCheck;
        GameController._gC.OnCardOutCooldown -= MoveOutCooldown;
        GameController._gC.OnCardConditionFalse -= ActionFalseAnimation;
        GameController._gC.OnCardRefreshed -= RefreshCard;
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
            ChangeScale();
        }
    }
    
    public void ChangeScale()
    {
        transform.DOScale(0.8F, 0.25F).OnComplete(ChangeScaleBack);
    }

    public void ChangeScaleBack()
    {
        transform.DOScale(0.9F, 0.25F).OnComplete(ChangeColorToPlayed);
    }

    public void UnfreezeCard()
    {
        Debug.Log("Unfreezing!");
        GameController._gC.UnfreezeCard(this);
        
    }

    public void RefreshCard(CardModel Model)
    {
        if (model == Model)
        {
            ChangeColorToNotPlayed();
        }
    }

    public void ChangeColorToPlayed()
    {
        //StartCoroutine(GrayscaleRoutine(1,true));
        
        bottomRenderer.color = new Color(0F, 0F, 0F);
        topRenderer.color = new Color(0F, 0F, 0F);
        actionRenderer.color = new Color(0.5F, 0.5F, 0.5F);
        coolDown.GetComponent<TextMeshPro>().color = new Color(0F, 0.6509804F, 0.6901961F);
        coolDown.transform.DOScale(2F, 0.5f).OnComplete(CoolDownTextBack);
        //UpdateCooldown();
    }

    public void ChangeColorToNotPlayed()
    {
        bottomRenderer.color = new Color(0F, 0.65F, 0.69F);
        topRenderer.color = new Color(1F, 0F, 0F);
        actionRenderer.color = new Color(1F, 1F, 1F);
        coolDown.GetComponent<TextMeshPro>().color = new Color(1F, 1F, 1F);
        UpdateCooldown();
    }

    public void MoveOutCooldown(CardModel Model)
    {
        if (model == Model)
        {
            ChangeColorToNotPlayed();
            //StartCoroutine(GrayscaleRoutine(1,false));    
        }
    }

    public void ActionFalseAnimation(CardModel Model)
    {
        if (model == Model)
        {
            actionCard.GetComponent<SpriteRenderer>().DOColor(new Color(1F, 0F, 0F), 0.5F)
                .OnComplete(ActionFalseAnimationBack);
        }
    }

    public void ActionFalseAnimationBack()
    {
        actionCard.GetComponent<SpriteRenderer>().DOColor(new Color(1F, 1F, 1F), 0.5F);
    }

    private IEnumerator GrayscaleRoutine(float duration, bool isGrayscale)
    {
        float time = 0;
        while (duration > time)
        {
            float durationFrame = Time.deltaTime;
            float ratio = time / duration;
            float grayAmount = isGrayscale ? ratio : 1 - ratio;
            SetGrayscale(grayAmount);
            time += durationFrame;
            yield return null;
        }
        
        SetGrayscale(isGrayscale? 1:0);
    }

    public void SetGrayscale(float amount = 1)
    {
        baseRenderer.material.SetFloat("_GrayscaleAmount",amount);
        topRenderer.material.SetFloat("_GrayscaleAmount",amount);
        bottomRenderer.material.SetFloat("_GrayscaleAmount",amount);
        actionRenderer.material.SetFloat("_GrayscaleAmount",amount);
        if (amount == 1)
        {
            bottom.GetComponent<SpriteRenderer>().color = new Color(0F, 0F, 0F);
            top.GetComponent<SpriteRenderer>().color = new Color(0F, 0F, 0F);
            coolDown.GetComponent<TextMeshPro>().color = new Color(0F, 0.6509804F, 0.6901961F);
        }
        else
        {
            bottom.GetComponent<SpriteRenderer>().color = new Color(0F, 0.65F, 0.69F);
            top.GetComponent<SpriteRenderer>().color = new Color(1F, 0F, 0F);
            coolDown.GetComponent<TextMeshPro>().color = new Color(1F, 1F, 1F);
        }

        UpdateCooldown();
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
            //button.SetActive(false);
        }
        else
        {
            SetupActionSprite();
            SetupBaseCard();
            UpdateActionAmount();
            UpdateCooldown();
            cardState.SetActive(false);
            UpdateButtonView();
        }
    }

    public void UpdateButtonView()
    {
        upgradeCost.text = model.upgradeCost.ToString();
        refreshCost.text = model.cooldownRemoveCost.ToString();
        
    }

    public void UpdateCooldownCheck(CardModel Model)
    {
        if (model == Model)
        {
            UpdateCooldown();
        }
    }

    public void UpdateCooldown()
    {
        if (model.action.coolDown == 0)
        {
            bottom.SetActive(false);
        }
        else
        {
            bottom.SetActive(true);
            if (model.action.coolDownLeft == 0)
            {
                coolDown.GetComponent<TextMeshPro>().text = model.action.coolDown.ToString();
                buttonRefresh.SetActive(false);
            }
            else
            {
                coolDown.GetComponent<TextMeshPro>().text = model.action.coolDownLeft.ToString();
                buttonRefresh.SetActive(true);
            }

            coolDown.transform.DOScale(2F, 0.5f).OnComplete(CoolDownTextBack);    
        }
    }

    public void CoolDownTextBack()
    {
        coolDown.transform.DOScale(1f, 0.2F);
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
                actionCard.GetComponent<SpriteRenderer>().color = new Color(1F, 0.7F, 0F);
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

}
