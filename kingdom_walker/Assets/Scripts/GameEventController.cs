using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventController : MonoBehaviour
{
    public GameObject tutorialMessagePrefab;

    public GameObject arrowPrefab;

    public GameObject tapPrefab;

    public GameObject currentMessage;

    public List<GameObject> currentArrows;
    
    public List<GameObject> currentTaps;


    public void InstantiateMessage()
    {
        foreach (TutorialMessageModel model in GameController._gC._game._tutorialMessageModels)
        {
            if (model.turnAppear == GameController._gC._game._turnNumber)
            {
                DestroyCurrentMessage();
                RectTransform canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
                currentMessage = Instantiate(tutorialMessagePrefab, canvas, true);
                currentMessage.GetComponent<TutorialMessageView>().SetupMessage(model);

                if (model.element == AdditionalElement.arrow)
                {
                    if (model.elementPlace == AdditionalElementPlace.redCastleTile)
                    {
                        foreach (GameObject tile in GameController._gC._fieldC.tiles)
                        {
                            if (tile.GetComponent<TileView>().model.tileType == TileType.RedTower)
                            {
                                GameObject currentArrow = Instantiate(arrowPrefab, canvas, true);
                                
                                Vector3 position = Camera.main.WorldToScreenPoint(tile.transform.position);
                                
                                float x = tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
                                
                                currentArrow.GetComponent<RectTransform>().localPosition =
                                    new Vector3(position.x - Screen.width/2F + x*100F, 
                                        position.y - Screen.height/2F);
                                
                                currentArrows.Add(currentArrow);
                            }
                        }
                    }

                    if (model.elementPlace == AdditionalElementPlace.enemy)
                    {
                        foreach (GameObject avatar in GameController._gC._fieldC.avatars)
                        {
                            if (avatar.GetComponent<AvatarView>().model.isPlayer != true)
                            {
                                GameObject currentArrow = Instantiate(arrowPrefab, canvas, true);
                                
                                Vector3 position = Camera.main.WorldToScreenPoint(avatar.transform.position);
                                
                                //float x = avatar.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
                                
                                currentArrow.GetComponent<RectTransform>().localPosition =
                                    new Vector3(position.x - Screen.width/2F + 100F, 
                                                position.y - Screen.height/2F);
                                
                                currentArrows.Add(currentArrow);
                            }
                        }
                    }
                    
                    if (model.elementPlace == AdditionalElementPlace.moveTile)
                    {
                        currentTaps = new List<GameObject>();
                        
                        foreach (GameObject tile in GameController._gC._fieldC.tiles)
                        {
                            if (tile.GetComponent<TileView>().model.availableForMove)
                            {
                                GameObject instance = Instantiate(arrowPrefab, canvas,true);
                                
                                Vector3 position = Camera.main.WorldToScreenPoint(tile.transform.position);

                                float x = tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x;

                                instance.GetComponent<RectTransform>().localPosition = 
                                    new Vector3(position.x - Screen.width/2F + x*100F, 
                                                position.y - Screen.height/2F);

                                currentArrows.Add(instance);
                            }
                        }
                    }
                    if (model.elementPlace == AdditionalElementPlace.playerCard)
                    {
                        currentTaps = new List<GameObject>();
                        
                        //Debug.Log("Before Cards");
                        foreach (GameObject card in GameController._gC._fieldC.playerCards)
                        {
                            
                            if (card.GetComponent<CardView>().model != null)
                            {
                                GameObject instance = Instantiate(arrowPrefab, canvas,true);

                                Vector3 position = Camera.main.WorldToScreenPoint(card.transform.position);

                                instance.GetComponent<RectTransform>().localPosition = new Vector3(position.x - Screen.width/2F + 100F, 
                                    position.y - Screen.height/2F);
                                
                                currentArrows.Add(instance);
                            }
                        }
                        //Debug.Log("After Cards");
                        
                    }
                }
            }
        }
    }

    public void DestroyCurrentMessage()
    {
        if (currentMessage)
        {
            Destroy(currentMessage);
            currentMessage = null;
            foreach (GameObject arrow in currentArrows)
            {
                Destroy(arrow);
            }
            currentArrows.Clear();
            foreach (GameObject tap in currentTaps)
            {
                Destroy(tap);
            }
            currentTaps.Clear();
        } 
    }
    
    
}
