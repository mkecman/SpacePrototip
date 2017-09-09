using System;
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
        UpdateLabel();
    }

    public void handleGameModelLoaded( AbstractMessage message )
    {
        float SC = 0;
        int atomsLength = gameModel.User.Atoms.Count;

        foreach( KeyValuePair<int, AtomModel> atomModel in gameModel.User.Atoms )
        {
            SC += atomModel.Value.Stock * atomModel.Value.AtomicWeight;
        }

        gameModel.User.SC = SC;
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
        SCHUDLabel.text = "SC:" + (int)gameModel.User.SC;
    }

   
}
