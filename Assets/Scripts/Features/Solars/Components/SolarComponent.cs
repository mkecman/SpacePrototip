using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolarComponent : MonoBehaviour
{
    public GameObject PlanetPrefab;
    public Transform PlanetsContainer;

    private SolarModel _model;
    private StoreComponent _solarStore;
    private float _lastTime;
    private bool _isActive;

    void Update()
    {
        if ( _isActive && Time.time - _lastTime > 1.0f )
        {
            _lastTime = Time.time;

            if ( _solarStore.Stock > 0 )
            {
                _model.Lifetime -= 1;
                _solarStore.Stock -= 1;
            }
            else
            {
                Messenger.Dispatch( SolarMessage.SOLAR_DESTROYED, new SolarMessage( 0, 0, _model ) );
                Destroy(gameObject);
            }
        }
    }

    public void Setup( SolarModel model )
    {
        _model = model;
        
        _solarStore = GetComponent<StoreComponent>();
        _solarStore.Name = _model.Name;
        _solarStore.MaxStock = _model.Lifetime;
        _solarStore.Stock = _model.Lifetime;
        _solarStore.Property = _model.Radius + "";
        
        for( int index = 0; index < _model.Planets.Count; index++ )
        {
            GameObject planet = Instantiate( PlanetPrefab, PlanetsContainer );
            PlanetComponent planetView = planet.GetComponent<PlanetComponent>();
            planetView.Setup( _model.Planets[ index ] );
        }

        _isActive = true;
    }

    private void OnDestroy()
    {
        _model = null;
        _solarStore = null;
        _isActive = false;
    }
}
