using System;
using UniRx;
using UnityEngine.UI;

public class PlanetAtomUpgradeComponent : AbstractView
{
    public Text UIStockLabel;
    public Text UIPriceLabel;
    public Button UIUpgradeButton;

    private AtomModel _model;

    public void UpdateModel( AtomModel model )
    {
        _model = model;

        UIUpgradeButton.OnClickAsObservable()
        .Subscribe( _ => Messenger.Dispatch( HarvesterMessage.HARVESTER_UPGRADE, new HarvesterMessage( _model ) ) )
        .AddTo( this );

        gameModel.User.rHC
        .Subscribe( HC => UIUpgradeButton.interactable = HC >= _model.HarvestRateUpgradePrice )
        .AddTo( this );
        
        _model.rHarvestRate
        .Subscribe( HR => UIStockLabel.text = _model.HarvestRate.ToString( "F1" ) + "AM/s" )
        .AddTo( this );
        
        _model.rHarvestRateUpgradePrice
        .Subscribe( price => UIPriceLabel.text = _model.HarvestRateUpgradePrice.ToString() )
        .AddTo( this );
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
