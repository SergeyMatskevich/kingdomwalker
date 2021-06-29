using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMessageModel
{
    public int turnAppear;
    public string messageToShow;
    public ScreenLocation location;
    public AdditionalElement element;
    public AdditionalElementPlace elementPlace;

    public TutorialMessageModel(int turn, string message, ScreenLocation loc, AdditionalElement el = AdditionalElement.none, AdditionalElementPlace place = AdditionalElementPlace.none)
    {
        turnAppear = turn;
        messageToShow = message;
        location = loc;
        element = el;
        elementPlace = place;
    }
}
