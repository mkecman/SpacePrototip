using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPManager : AbstractController
{
    private Text label;

    void Start()
    {
        label = gameObject.GetComponent<Text>();
        Messenger.Listen( SolarMessage.SOLAR_CREATED, handleSolarCreated );
    }
    
    private void handleSolarCreated( AbstractMessage message )
    {
        gameModel.User.XP += 1;
        updateView();
    }

    private void updateView()
    {
        label.text = "XP:" + gameModel.User.XP;
    }
    
}
