using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HCManager : AbstractController
{
    private Text label;

    void Start()
    {
        label = gameObject.GetComponent<Text>();
        Messenger.Listen( HCMessage.UPDATED, handleHCUpdate );
    }
    
    private void handleHCUpdate( AbstractMessage message )
    {
        HCMessage hcMessage = message as HCMessage;
        gameModel.User.HC += hcMessage.Amount;
        updateView();
    }

    private void updateView()
    {
        label.text = "HC:" + gameModel.User.HC;
    }
    
}
