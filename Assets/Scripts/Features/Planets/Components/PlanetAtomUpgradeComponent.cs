using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using UniRx;

public class PlanetAtomUpgradeComponent : AbstractView
{
    public Text UIStockLabel;
    public Text UIPriceLabel;
    public Button UIUpgradeButton;
    
    private AtomModel _model;
    
    void Start()
    {
        UIUpgradeButton.OnClickAsObservable()
            .Subscribe( _ => Messenger.Dispatch(HarvesterMessage.HARVESTER_UPGRADE, new HarvesterMessage(_model)) )
            .AddTo(this);

        gameModel.User.rHC
            .Subscribe(HC => UIUpgradeButton.interactable = HC >= _model.HarvestRateUpgradePrice )
            .AddTo(this);
    }
    
    public void UpdateModel( AtomModel model )
    {
        _model = model;

        _model.rHarvestRate
            .Subscribe(HR => UIStockLabel.text = (1 / (1 / _model.HarvestRate)).ToString("F1") + "/s")
            .AddTo(this);

        _model.rHarvestRateUpgradePrice
            .Subscribe(price => UIPriceLabel.text = _model.HarvestRateUpgradePrice.ToString())
            .AddTo(this);
    }

    private void Upgrade()
    {
        Messenger.Dispatch( HarvesterMessage.HARVESTER_UPGRADE, new HarvesterMessage( _model ) );
    }

    void OnDestroy()
    {
        _model = null;
    }
}
