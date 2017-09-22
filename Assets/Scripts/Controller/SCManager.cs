﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCManager : AbstractController
{
    public Text SCHUDLabel;
    public Slider SCSlider;
    public Text SCText;

    void Start()
    {
        Messenger.Listen( AtomMessage.ATOM_STOCK_UPDATED, handleAtomStockUpdated );
        Messenger.Listen( GameMessage.MODEL_LOADED, handleGameModelLoaded );
        Messenger.Listen( SolarMessage.SOLAR_CREATED, handleSolarCreated );
    }

    private void handleSolarCreated( AbstractMessage message )
    {
        UpdateLabel();
    }

    public void handleAtomStockUpdated( AbstractMessage message )
    {
        AtomMessage data = message as AtomMessage;
        gameModel.User.SC += gameModel.Atoms[ data.AtomicNumber ].AtomicWeight * data.Delta;
        if( gameModel.User.SC > SCSlider.maxValue )
            if( gameModel.User.SC <= gameModel.maxSC )
                SCSlider.maxValue = gameModel.User.SC;
            else
                SCSlider.maxValue = gameModel.maxSC;

        UpdateLabel();
    }

    public void handleGameModelLoaded( AbstractMessage message )
    {
        float SC = 0;
        
        foreach( AtomModel atomModel in gameModel.User.Atoms )
        {
            SC += atomModel.Stock * atomModel.AtomicWeight;
        }
        
        gameModel.User.SC = SC;
        SCSlider.minValue = gameModel.minSC + 2;
        SCSlider.maxValue = SC;
        SCSlider.value = SC;
        UpdateLabel();
    }

    public void CreateSolar()
    {
        Messenger.Dispatch( SolarMessage.CREATE_SOLAR, new SolarMessage( SCSlider.value ) );
    }

    public void UpdateSCSliderLabel()
    {
        SCText.text = SCSlider.value + "";
    }

    private void UpdateLabel()
    {
        SCHUDLabel.text = "AM:" + (int)gameModel.User.SC;
    }

   
}
