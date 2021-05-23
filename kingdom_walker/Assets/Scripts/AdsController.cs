using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


public class AdsController : MonoBehaviour
{
    string gameId = "3556843";
    bool testMode = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowInterstitial()
    {
        //Advertisement.Initialize(gameId, false);
        // Show an ad:
        //Advertisement.Show("LevelCompleted");
    }
}
