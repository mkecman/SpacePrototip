using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCManager : AbstractController
{
    public Text SCHUDLabel;
    public AtomSliderComponent SCSlider;
    public Text SCText;
    private float _lastTime;
    private bool _isActive = false;

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

    void Update()
    {

        if( _isActive && Time.time - _lastTime > 1.0f )
        {
            if( SCSlider.maxValue >= gameModel.Config.maxSC )
                _isActive = false;

            SCSlider.maxValue += 10;
        }
    }

    public void handleAtomStockUpdated( AbstractMessage message )
    {
        AtomMessage data = message as AtomMessage;
        gameModel.User.SC += gameModel.Atoms[ data.AtomicNumber ].AtomicWeight * data.Delta;
        if( gameModel.User.SC > SCSlider.maxValue )
            if( gameModel.User.SC <= gameModel.Config.maxSC )
                SCSlider.maxValue = gameModel.User.SC;
            else
                SCSlider.maxValue = gameModel.Config.maxSC;

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
        SCSlider.Setup( SC );
        UpdateLabel();
        //_isActive = true;
    }

    public void CreateSolar()
    {
        Messenger.Dispatch( SolarMessage.CREATE_SOLAR, new SolarMessage( SCSlider.SCSlider.value, SCSlider.maxValue ) );
    }
    
    private void UpdateLabel()
    {
        SCHUDLabel.text = "AM:" + (int)gameModel.User.SC;
    }

   
}
