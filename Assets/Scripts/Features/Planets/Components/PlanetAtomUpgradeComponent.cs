﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlanetAtomUpgradeComponent : AbstractView
{
    public Text UIStockLabel;
    public Text UIPriceLabel;
    public Button UIUpgradeButton;
    
    private PlanetAtomModel _model;

    void Start()
    {
        UIUpgradeButton.onClick.AddListener( new UnityAction( Upgrade ) );
    }

    public void Setup( PlanetAtomModel model )
    {
        _model = model;

        updateView();
    }

    private int getPrice()
    {
        return (int)Mathf.Pow( gameModel.Atoms[ _model.AtomicNumber ].AtomicWeight, 1 );// * _model.HarvestRate;
    }

    private void Upgrade()
    {
        int price = getPrice();
        if( price > gameModel.User.HC )
            return;

        Messenger.Dispatch(HCMessage.UPDATED, new HCMessage(-price));
        _model.HarvestRate += 1;

        updateView();
    }

    private void updateView()
    {
        UIStockLabel.text = _model.HarvestRate.ToString();
        UIPriceLabel.text = getPrice() + "";
    }
}
