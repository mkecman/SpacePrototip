using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
using UniRx;
using UniRx.Triggers;

public class AtomStockUpgradeComponent : AbstractView
{
    public Text UIStockLabel;
    public Text UISCLabel;
    public Button UIButton;

    private AtomModel _model;
    private AtomMessage _atomMessage;

    void Start()
    {
        _atomMessage = new AtomMessage( _model.AtomicNumber, 1 );
        UIButton.OnClickAsObservable().Subscribe(_ => Messenger.Dispatch(AtomMessage.ATOM_STOCK_UPGRADE, _atomMessage) );
    }

    public void UpdateModel( AtomModel model )
    {
        _model = model;

        _model.rMaxStockNextLevel.Subscribe( newValue => UIStockLabel.text = "+" + newValue.ToString() );
        _model.rMaxStockUpgradePrice.Subscribe( newValue => UISCLabel.text = newValue.ToString() );

        gameModel.User.rSC.Where( SC => SC >= _model.MaxStockUpgradePrice ).Subscribe( _ => UIButton.interactable = true );
        gameModel.User.rSC.Where( SC => SC < _model.MaxStockUpgradePrice ).Subscribe( _ => UIButton.interactable = false );
    }
    
    private void DispatchUpgradeMessage()
    {
        Messenger.Dispatch( AtomMessage.ATOM_STOCK_UPGRADE, _atomMessage );
    }
}
