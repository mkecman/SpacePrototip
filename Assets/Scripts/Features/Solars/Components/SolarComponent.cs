using System;
using System.Collections.Generic;
using System.Text;
using UniRx;
using UnityEngine;

public class SolarComponent : MonoBehaviour
{
    public GameObject PlanetPrefab;
    public Transform PlanetsContainer;
    public StoreComponent SolarStore;

    private SolarModel _model;
    private StringBuilder sb = new StringBuilder();

    private List<PlanetComponent> planets;

    public void Setup( SolarModel model )
    {
        _model = model;
        
        SolarStore.Name = _model.Name;
        SolarStore.MaxStock = _model.Lifetime;
        SolarStore.Stock = _model.Lifetime;
        SolarStore.Property = _model.Radius + "";

        planets = new List<PlanetComponent>();
        for( int index = 0; index < _model.Planets.Count; index++ )
        {
            GameObject planet = Instantiate( PlanetPrefab, PlanetsContainer );
            PlanetComponent planetView = planet.GetComponent<PlanetComponent>();
            planetView.Setup( _model.Planets[ index ] );
            planets.Add( planetView );
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
            .Subscribe( onLifetimeUpdate )
            .AddTo( this );
    }

    private void onLifetimeUpdate( int lifetime )
    {
        SolarStore.Stock = lifetime;
        SolarStore.Property = FormatTimeSpan( TimeSpan.FromSeconds( lifetime ) );
    }

    private string FormatTimeSpan( TimeSpan ts )
    {
        sb.Clear();
        if( (int)ts.TotalHours > 0 )
        {
            sb.Append( (int)ts.TotalHours );
            sb.Append( "h " );
        }

        if( ts.Minutes > 0 )
        {
            sb.Append( ts.ToString( @"mm" ) );
            sb.Append( "m " );
        }
        
        sb.Append( ts.ToString( @"ss" ) );
        sb.Append( "s" );

        return sb.ToString();
    }

    private void OnDestroy()
    {
        _model = null;
        SolarStore = null;
    }
}
