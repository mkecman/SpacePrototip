using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UniRx;

public class HarvesterComponent : AbstractView
{
    public Slider UISlider;
    
    private AtomModel _model;

    private float _startTime;
    private FloatReactiveProperty timePercent = new FloatReactiveProperty();
    private AtomMessage _atomMessage;
    
    internal void UpdateModel( AtomModel model )
    {
        _model = model;
        _startTime = Time.time;

        _atomMessage = new AtomMessage( _model.AtomicNumber, 1 );

        AddReactor( timePercent );

        AddReactor
        (
            Observable.EveryUpdate()
            .Subscribe( _ => timePercent.Value = ( Time.time - _startTime ) / ( 1f / _model.HarvestRate ) )
        );

        AddReactor
        (
            timePercent.Subscribe( percent => UISlider.value = UISlider.maxValue * percent )
        );
        
        AddReactor
        (
            timePercent
            .Where( percent => percent >= 1f )
            .Subscribe( percent =>
                {
                    _startTime = Time.time;
                    _model.Stock -= 1;
                    Messenger.Dispatch( AtomMessage.ATOM_HARVEST, _atomMessage );
                }
            )
        );

        AddReactor
        (
            _model.rStock.Where( _ => _model.Stock <= 0 ).Subscribe( _ => DestroyReactors() )
        );
        
    }
    
    void OnDestroy()
    {
        DestroyReactors();
        _model = null;
    }

}
