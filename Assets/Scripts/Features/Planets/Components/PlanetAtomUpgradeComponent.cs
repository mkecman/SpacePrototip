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
    
    private AtomModel _model;
    
    void Start()
    {
        UIUpgradeButton.onClick.AddListener( new UnityAction( Upgrade ) );
        Messenger.Listen( HCMessage.UPDATED, handleHCUpdated );
    }

    private void handleHCUpdated( AbstractMessage message )
    {
        if( gameModel.User.HC < _model.HarvestRateUpgradePrice )
        {
            UIUpgradeButton.interactable = false;
        }
        else
        {
            UIUpgradeButton.interactable = true;
        }
    }

    public void UpdateModel( AtomModel model )
    {
        _model = model;
        updateView();
    }
    
    private void handleHarvesterUpgraded( AbstractMessage message )
    {
        updateView();
    }

    private void Upgrade()
    {
        Messenger.Dispatch( HarvesterMessage.HARVESTER_UPGRADE, new HarvesterMessage( _model ) );
        updateView();
    }

    private void updateView()
    {
        UIStockLabel.text = ( 1 / ( 1 / _model.HarvestRate ) ).ToString("F1") + "/s";
        UIPriceLabel.text = _model.HarvestRateUpgradePrice.ToString();
    }

    void OnDestroy()
    {
        _model = null;
        UIUpgradeButton.onClick.RemoveListener( new UnityAction( Upgrade ) );
        Messenger.StopListening( HCMessage.UPDATED, handleHCUpdated );
    }
}
