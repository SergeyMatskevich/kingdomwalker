using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceView : MonoBehaviour
{
    private GamerModel model;
    
    public TextMeshPro coinsText;
    public TextMeshPro gemsText;

    private void Start()
    {
        GameController._gC.OnResourceUpdate += UpdateResource;
    }

    private void OnDestroy()
    {
        GameController._gC.OnResourceUpdate -= UpdateResource;
    }

    public void UpdateResource()
    {
        coinsText.text = model.coins.ToString();
        gemsText.text = model.gems.ToString();
    }

    public void SetupResource(GamerModel Model)
    {
        model = Model;
        if (model.coins == 0 && model.gems == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            coinsText.text = model.coins.ToString();
            gemsText.text = model.gems.ToString();    
        }
    }
}
