using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class RewardController : MonoBehaviour
{
    public GameObject rewardItemPrefab;
    public TextMeshProUGUI message;
    public GameObject rewardPanel;
    public RectTransform rewardItems;
    public List<GameObject> rewardItemObjects;

    public void SetupWinRewards()
    {
        ClearOldRewards();
        message.text = "Victory";
        message.color = new Color(1F,0.75F,0F,1F);
        rewardPanel.SetActive(true);
        
        foreach (RewardItemModel model in GameController._gC._game._winReward.items)
        {
            GameObject item = Instantiate(rewardItemPrefab, rewardItems, true);
            item.GetComponent<RewardItemView>().SetupRewardItemView(model);
        }
    }

    public void ClearOldRewards()
    {
        foreach (GameObject item in rewardItemObjects)
        {
            item.GetComponent<RewardItemView>().DestroyRewardItem();
        }
    }

}
