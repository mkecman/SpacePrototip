using System;
using UniRx;
using UnityEngine;

public class SolarComponent : MonoBehaviour
{
    public GameObject PlanetPrefab;
    public Transform PlanetsContainer;

    private SolarModel _model;
    private StoreComponent _solarStore;
    
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

        Observable.Interval( TimeSpan.FromSeconds( 1 ) )
            .Where( _ => _model.Lifetime > 0 )
            .Subscribe( _ => _model.Lifetime -= 1 )
            .AddTo(this);

        _model.rLifetime
            .Where( lifetime => lifetime == 0 )
            .Subscribe( _ =>
            {
                Messenger.Dispatch( SolarMessage.SOLAR_DESTROYED, new SolarMessage( 0, 0, _model ) );
                Destroy( gameObject );
            } )
            .AddTo(this);

        _model.rLifetime
            .Subscribe( lifetime => _solarStore.Stock = lifetime )
            .AddTo( this );
        
    }

    private void OnDestroy()
    {
        _model = null;
        _solarStore = null;
    }
}
