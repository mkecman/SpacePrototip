using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class SCManager : AbstractController
{
    public Text SCHUDLabel;
    public AtomSliderComponent SCSlider;
    public Text SCText;
    
    void Start()
    {
        Messenger.Listen( GameMessage.MODEL_LOADED, handleGameModelLoaded );
    }
    
    public void handleGameModelLoaded( AbstractMessage message )
    {
        gameModel.User.rSC.Subscribe( sc => SCHUDLabel.text = "AM:" + (int)Mathf.Floor( sc ) );
        gameModel.User.rSC
            .Where( SC => SC > SCSlider.maxValue )
            .Subscribe( SC =>
            {
                if( gameModel.User.SC <= gameModel.Config.maxSC )
                    SCSlider.maxValue = Mathf.Floor( gameModel.User.SC );
                else
                    SCSlider.maxValue = gameModel.Config.maxSC;
            } );
        
        SCSlider.Setup( gameModel.User.SC );
    }

    public void CreateSolar()
    {
        Messenger.Dispatch( SolarMessage.CREATE_SOLAR, new SolarMessage( SCSlider.SCSlider.value, SCSlider.maxValue ) );
    }
    
}
