using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RewardItemView : MonoBehaviour
{
    public Image itemImage;
    public Sprite coin, gem;
    public TextMeshProUGUI itemText;
    private RewardItemModel model;

    public void SetupRewardItemView(RewardItemModel Model)
    {
        model = Model;
        itemText.text = model.amount.ToString();
        switch (model.type)
        {
            case RewardItemType.Coin:
                itemImage.sprite = coin;
                break;
            case RewardItemType.Gem:
                itemImage.sprite = gem;
                break;
            
        }
    }

    public void DestroyRewardItem()
    {
        Destroy(gameObject);
    }
}
