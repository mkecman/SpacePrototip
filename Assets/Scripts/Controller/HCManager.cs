﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HCManager : AbstractController
{
    private Text label;

    void Start()
    {
        label = gameObject.GetComponent<Text>();
        Messenger.Listen( HCMessage.UPDATE_REQUEST, handleHCUpdate );
        Messenger.Listen( GameMessage.MODEL_LOADED, handleGameModelLoaded );
        Messenger.Listen( HarvesterMessage.HARVESTER_UPGRADE, handleHarvesterUpgrade );
    }

    private void handleHarvesterUpgrade( AbstractMessage message )
    {
        HarvesterMessage data = message as HarvesterMessage;
        if( gameModel.User.HC >= data.Model.HarvestRateUpgradePrice )
        {
            gameModel.User.HC -= data.Model.HarvestRateUpgradePrice;
            data.Model.HarvestRate += gameModel.Config.HarvestRateUpgradeStep;

            updateView();
            Messenger.Dispatch( HCMessage.UPDATED );
        }
    }

    public void handleGameModelLoaded( AbstractMessage message )
    {
        updateView();
    }

    private void handleHCUpdate( AbstractMessage message )
    {
        HCMessage hcMessage = message as HCMessage;
        gameModel.User.HC += hcMessage.Amount;
        Messenger.Dispatch( HCMessage.UPDATED );
        updateView();
    }

    private void updateView()
    {
        label.text = "HC:" + gameModel.User.HC;
    }
    
}
