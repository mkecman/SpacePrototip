using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarComponent : MonoBehaviour
{
    private SolarModel _model;
    public Transform _planetsContainer;  
    
    void Start()
    {
        

    }

	public void Setup( SolarModel model )
    {
        _model = model;
        _planetsContainer = this.transform.Find( "PlanetsContainer" );

        StoreComponent solarStore = GetComponent<StoreComponent>();
        solarStore.Name = _model.Name;
        solarStore.MaxStock = _model.Lifetime;
        solarStore.Stock = _model.Lifetime;
        solarStore.Property = _model.Radius + "";

        Messenger.Dispatch( PlanetMessage.CREATE_PLANET, new PlanetMessage( _model.Planets, _planetsContainer ) );
    }
}
