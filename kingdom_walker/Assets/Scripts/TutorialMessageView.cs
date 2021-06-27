using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialMessageView : MonoBehaviour
{
    public TextMeshProUGUI message;
    private TutorialMessageModel model;

    public void SetupMessage(TutorialMessageModel Model)
    {
        model = Model;
        message.text = model.messageToShow;
        message.color = new Color(0F,0F,0F,1F);

        RectTransform rect = GetComponent<RectTransform>();
        

        if (model.location == ScreenLocation.Bottom)
        {
            rect.anchoredPosition = new Vector2(0F, (Screen.height / 4 + 50) * -1);
        }
        else
        {
            rect.anchoredPosition = new Vector2(0F, (Screen.height/4 + 50F));
        }
    }
}
