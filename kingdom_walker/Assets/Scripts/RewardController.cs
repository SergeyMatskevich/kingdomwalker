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
    public GameObject claimButton;
    public GameObject againButton;
    public GameObject doubleButton;

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
            rewardItemObjects.Add(item);
        }
        
        claimButton.SetActive(true);
        againButton.SetActive(false);
        doubleButton.SetActive(true);
    }

    public void SetupDefeatRewards()
    {
        ClearOldRewards();
        message.text = "Defeat";
        message.color = new Color(1F,0F,0F,1F);
        rewardPanel.SetActive(true);

        if (GameController._gC._game._loseReward.items.Count > 0)
        {
            foreach (RewardItemModel model in GameController._gC._game._loseReward.items)
            {
                GameObject item = Instantiate(rewardItemPrefab, rewardItems, true);
                item.GetComponent<RewardItemView>().SetupRewardItemView(model);
                rewardItemObjects.Add(item);
            } 
        }
        
        claimButton.SetActive(false);
        againButton.SetActive(true);
        doubleButton.SetActive(true);
    }

    public void ClearOldRewards()
    {
        if (rewardItemObjects.Count > 0)
        {
            foreach (GameObject item in rewardItemObjects)
            {
                item.GetComponent<RewardItemView>().DestroyRewardItem();
            }
            
            rewardItemObjects.Clear();
        }
    }

}
