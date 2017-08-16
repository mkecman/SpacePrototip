﻿using UnityEngine;
using UnityEngine.UI;


public class GameController : AbstractController
{
    public Slider SCSlider;
    public Text SCText;
    
    public void Setup()
    {
        Messenger.Dispatch( GameMessage.MODEL_LOADED );
        Messenger.Dispatch( AtomMessage.SETUP_ATOMS );
    }
    
    public void GenerateAtom()
    {
        Messenger.Dispatch( AtomMessage.GENERATE_ATOM );
    }

    public void CreateSolar()
    {
        Messenger.Dispatch( SolarMessage.CREATE_SOLAR, new SolarMessage( SCSlider.value ) );
    }

    public void UpdateSCLabel()
    {
        SCText.text = SCSlider.value + "";
    }
}