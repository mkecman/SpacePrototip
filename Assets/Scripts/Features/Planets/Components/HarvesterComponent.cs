using System;
using UniRx;
using UnityEngine;

public class HarvesterComponent : AbstractView
{
    public ProgressBar UISlider;

    private AtomModel _model;

    private float _startTime;
    private FloatReactiveProperty timePercent = new FloatReactiveProperty();
    private AtomMessage _atomMessage;
    private IDisposable _interval;
    private float _harvestTime = 1f;

    internal void UpdateModel( AtomModel model )
    {
        _model = model;
        _startTime = Time.time;
        
        _atomMessage = new AtomMessage( _model.AtomicNumber, 1 );

        AddReactor( timePercent );
        
        AddReactor
        (
            _model.rHarvestRate
            .Subscribe( rate =>
            {
                _harvestTime = _model.AtomicWeight / _model.HarvestRate;
                _startTime = -( timePercent.Value * _harvestTime ) + Time.time;
            })
        );

        AddReactor
        (
            Observable.EveryUpdate()
                .Subscribe( _ => timePercent.Value = ( Time.time - _startTime ) / _harvestTime )
        );

        AddReactor
        (
            timePercent.Subscribe( percent => UISlider.value = UISlider.maxValue * percent )
        );
        
        AddReactor
        (
            _model.rStock.Where( _ => _model.Stock <= 0 ).Subscribe( _ => DestroyReactors() )
        );

        AddReactor
        (
            timePercent
            .Where( value => value >= 1f )
            .Subscribe( _ =>
            {
                _startTime = Time.time;
                _model.Stock -= 1;
                Messenger.Dispatch( AtomMessage.ATOM_HARVEST, _atomMessage );
            } )
        );

    }
    
    void OnDestroy()
    {
        DestroyReactors();
        _model = null;
    }

}
