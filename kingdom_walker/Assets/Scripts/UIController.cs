using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject nextLvlButton, replayButton, backgroundPanel, result, desctiption, playerHealth, enemyHealth;

    public TextMeshProUGUI resultMessage;
    public TextMeshProUGUI descriptionMessage;

    public void HideUI()
    {
        nextLvlButton.SetActive(false);
        replayButton.SetActive(false);
        backgroundPanel.SetActive(false);
        result.SetActive(false);
        desctiption.SetActive(false);
        playerHealth.SetActive(true);
        enemyHealth.SetActive(true);
    }

    public void ShowResultScreen(bool win, int lvl)
    {
        if (win && lvl != 10)
        {
            nextLvlButton.SetActive(true);
        }
        replayButton.SetActive(true);
        backgroundPanel.SetActive(true);
        result.SetActive(true);
        desctiption.SetActive(true);
        playerHealth.SetActive(false);
        enemyHealth.SetActive(false);
    }

    public void SetResultMessage(string result, string description)
    {
        resultMessage.text = result;
        descriptionMessage.text = description;
    }

    public void RestartLevel()
    {
        
        GameController._gC.InitLevel();
    }

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
