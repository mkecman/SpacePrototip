using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class PlanetAtomUpgradeComponent : AbstractView
{
    public Text UIStockLabel;
    public Text UIPriceLabel;
    public Button UIUpgradeButton;
    
    private PlanetAtomModel _model;
    private int _currentPrice;

    void Start()
    {
        UIUpgradeButton.onClick.AddListener( new UnityAction( Upgrade ) );
        Messenger.Listen( HCMessage.UPDATED, handleHCUpdated );
    }

    private void handleHCUpdated( AbstractMessage message )
    {
        if( gameModel.User.HC < _currentPrice )
        {
            UIUpgradeButton.interactable = false;
        }
        else
        {
            UIUpgradeButton.interactable = true;
        }
    }

    public void Setup( PlanetAtomModel model )
    {
        _model = model;
        _currentPrice = getPrice();
        updateView();
    }

    private int getPrice()
    {
        return (int)(Mathf.Pow( 3f, _model.HarvestRate ) * gameModel.Atoms[ _model.AtomicNumber ].AtomicWeight);
    }

    private void Upgrade()
    {
        if( gameModel.User.HC >= _currentPrice )
        {

            Messenger.Dispatch( HCMessage.UPDATE_REQUEST, new HCMessage( -_currentPrice ) );
            _model.HarvestRate += gameModel.Config.HarvestRateUpgradeStep;
            _currentPrice = getPrice();
            updateView();



            Debug.Log( _model.HarvestRate );
        }
    }

    private void updateView()
    {
        UIStockLabel.text = ( 1 / ( gameModel.Config.MaxHarvestTime / _model.HarvestRate ) ).ToString("F1") + "/s";
        UIPriceLabel.text = _currentPrice.ToString();
    }
}
