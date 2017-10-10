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
        UIButton.OnClickAsObservable().Subscribe(_ => Messenger.Dispatch(AtomMessage.ATOM_STOCK_UPGRADE, _atomMessage) ).AddTo(this);
    }

    public void UpdateModel( AtomModel model )
    {
        _model = model;
        _atomMessage = new AtomMessage( _model.AtomicNumber, 1 );

        _model.rMaxStockNextLevel.Subscribe( newValue => UIStockLabel.text = "+" + newValue.ToString() ).AddTo(this);
        _model.rMaxStockUpgradePrice.Subscribe( newValue => UISCLabel.text = newValue.ToString() ).AddTo(this);

        gameModel.User.rSC.Subscribe( SC => UIButton.interactable = SC >= _model.MaxStockUpgradePrice ).AddTo(this);
    }
}
