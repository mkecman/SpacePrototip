using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class AtomStockUpgradeComponent : AbstractView
{
    public Text UIStockLabel;
    public Text UISCLabel;
    public Button UIButton;

    private AtomModel _model;
    private AtomMessage _atomMessage;

    void Start()
    {
        UIButton.onClick.AddListener( new UnityAction( DispatchUpgradeMessage ) );
        Messenger.Listen( AtomMessage.ATOM_STOCK_UPDATED, handleAtomStockUpdated );
        _atomMessage = new AtomMessage( _model.AtomicNumber, 1 );
    }

    public void UpdateModel( AtomModel model )
    {
        _model = model;

        UpdateView();
    }

    public void UpdateView()
    {
        UIStockLabel.text = "+" + _model.MaxStockNextLevel.ToString();
        UISCLabel.text = _model.MaxStockUpgradePrice.ToString();
    }

    private void handleAtomStockUpdated( AbstractMessage message )
    {
        if( gameModel.User.SC < _model.MaxStockUpgradePrice )
        {
            UIButton.interactable = false;
        }
        else
        {
            UIButton.interactable = true;
        }
    }
    
    private void DispatchUpgradeMessage()
    {
        Messenger.Dispatch( AtomMessage.ATOM_STOCK_UPGRADE, _atomMessage );
    }
}
