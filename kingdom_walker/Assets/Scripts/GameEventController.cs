using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventController : MonoBehaviour
{
    public GameObject tutorialMessagePrefab;

    public GameObject arrowPrefab;

    public GameObject tapPrefab;

    public GameObject currentMessage;

    public GameObject currentArrow;
    public List<GameObject> currentTaps;

    public void InstantiateArrow(Vector3 targetArrow)
    {
    }

    public void InstantiateTap(Vector3 targetTap)
    {
        
    }


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
                                currentArrow = Instantiate(arrowPrefab, canvas, true);
                                currentArrow.GetComponent<RectTransform>().localPosition =
                                    new Vector3(tile.transform.position.x * 100F + 150F,
                                        tile.transform.position.y * 100F);
                            }
                        }
                    }

                    if (model.elementPlace == AdditionalElementPlace.enemy)
                    {
                        foreach (GameObject avatar in GameController._gC._fieldC.avatars)
                        {
                            if (avatar.GetComponent<AvatarView>().model.isPlayer != true)
                            {
                                currentArrow = Instantiate(arrowPrefab, canvas, true);
                                currentArrow.GetComponent<RectTransform>().localPosition =
                                    new Vector3(avatar.transform.position.x * 100F + 150F,
                                    avatar.transform.position.y * 100F);
                            }
                        }
                    }
                }

                if (model.element == AdditionalElement.tap)
                {
                    if (model.elementPlace == AdditionalElementPlace.moveTile)
                    {
                        currentTaps = new List<GameObject>();
                        
                        foreach (GameObject tile in GameController._gC._fieldC.tiles)
                        {
                            if (tile.GetComponent<TileView>().model.availableForMove)
                            {
                                GameObject instance = Instantiate(tapPrefab, canvas,true);

                                instance.GetComponent<RectTransform>().localPosition =
                                    new Vector3(tile.transform.position.x * 100F, tile.transform.position.y * 100F);
                                
                                currentTaps.Add(instance);
                            }
                        }
                    }
                    
                    if (model.elementPlace == AdditionalElementPlace.playerCard)
                    {
                        currentTaps = new List<GameObject>();
                        
                        foreach (GameObject card in GameController._gC._fieldC.playerCards)
                        {
                            if (card.GetComponent<CardView>().model != null)
                            {
                                GameObject instance = Instantiate(tapPrefab, canvas,true);
                                
                                instance.GetComponent<RectTransform>().localPosition =
                                    new Vector3(card.transform.position.x * 100F, card.transform.position.y * 100F);
                                
                                currentTaps.Add(instance);
                            }
                        }
                    }
                    
                }
            }
        }
    }

    public void DestroyCurrentMessage()
    {
        if (currentMessage)
        {
            currentMessage.GetComponent<TutorialMessageView>().DestroyObject();
            currentMessage = null;
            Destroy(currentArrow);
            foreach (GameObject tap in currentTaps)
            {
                Destroy(tap);
            }
            currentArrow = null;
            currentTaps.Clear();
        } 
    }
    
    
}
