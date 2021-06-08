using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RewardItemView : MonoBehaviour
{
    public Image itemImage;
    public TextMeshProUGUI itemText;
    private RewardItemModel model;

    public void SetupRewardItemView(RewardItemModel Model)
    {
        model = Model;
        itemText.text = model.amount.ToString();
    }

    public void DestroyRewardItem()
    {
        Destroy(gameObject);
    }
}
