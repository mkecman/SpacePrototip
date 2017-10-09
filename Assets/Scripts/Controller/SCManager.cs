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
    private float _lastTime;
    private bool _isActive = false;

    

    void Start()
    {
        Messenger.Listen(AtomMessage.ATOM_STOCK_UPDATED, handleAtomStockUpdated);
        Messenger.Listen(GameMessage.MODEL_LOADED, handleGameModelLoaded);        
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
        if( gameModel.User.SC > SCSlider.maxValue )
            if( gameModel.User.SC <= gameModel.Config.maxSC )
                SCSlider.maxValue = Mathf.Floor( gameModel.User.SC );
            else
                SCSlider.maxValue = gameModel.Config.maxSC;

    }

    public void handleGameModelLoaded( AbstractMessage message )
    {
        gameModel.User.rSC.Subscribe(UpdateLabel);

        float SC = 0;
        
        foreach( AtomModel atomModel in gameModel.User.Atoms )
        {
            SC += atomModel.Stock * atomModel.AtomicWeight;
        }

        gameModel.User.SC = SC;
        SCSlider.Setup( SC );
        //_isActive = true;
    }

    public void CreateSolar()
    {
        Messenger.Dispatch( SolarMessage.CREATE_SOLAR, new SolarMessage( SCSlider.SCSlider.value, SCSlider.maxValue ) );
    }
    
    private void UpdateLabel( float sc )
    {
        SCHUDLabel.text = "AM:" + (int)Mathf.Floor( sc );
    }

   
}
