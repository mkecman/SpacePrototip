using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarView : MonoBehaviour
{
    public GameObject planetPrefab;
    public List<PlanetView> planetViews = new List<PlanetView>();

    private SolarModel _model;
    private Transform _planetsContainer;  
    

    void Start()
    {
        

    }

	public void SetupView( SolarModel model )
    {
        _model = model;

        _planetsContainer = this.transform.Find( "PlanetsContainer" );

        StoreView solarStore = GetComponent<StoreView>();
        solarStore.Name = _model.Name;
        solarStore.MaxStock = _model.Lifetime;
        solarStore.Stock = _model.Lifetime;
        solarStore.Property = _model.Radius + "";

        for( int index = 0; index < _model.Planets.Count; index++ )
        {
            CreatePlanet( _model.Planets[ index ] );
        }
    }

    private void CreatePlanet( PlanetModel model )
    {
        GameObject planet = Instantiate( planetPrefab, _planetsContainer );
        PlanetView planetView = planet.GetComponent<PlanetView>();
        planetView.SetupView( model );
        planetViews.Add( planetView );
    }
}
