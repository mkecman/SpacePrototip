using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class HarvesterComponent : AbstractView
{
    public Slider UISlider;
    
    private bool _isHarvesting = false;
    private PlanetAtomModel _model;
    private StoreComponent _store;
    private float _startTime;
    
    void Update()
    {
        if( !_isHarvesting )
            return;

        float maxPercent = gameModel.Config.MaxHarvestTime / _model.HarvestRate;
        float timePercent = ( Time.time - _startTime ) / maxPercent;
        
        UISlider.value = UISlider.maxValue * timePercent;
        
        if( timePercent >= 1.0f )
        {
            _startTime = Time.time;
            
            if( _model.Stock > 0 )
            {
                _store.Stock -= 1;
                _model.Stock -= 1;
                Messenger.Dispatch( AtomMessage.ATOM_HARVEST, new AtomMessage( _model.AtomicNumber, 1 ) );
                UISlider.value = 0;
            }
            else
            {
                _isHarvesting = false;
            }
        }
    }

    internal void Setup( StoreComponent store, PlanetAtomModel model )
    {
        _store = store;
        _model = model;
        _isHarvesting = true;
        _startTime = Time.time;
    }
    
}
