using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolarComponent : MonoBehaviour
{
    private SolarModel _model;
    private StoreComponent solarStore;
    public Transform _planetsContainer;
    private float _lastTime;
    private bool isActive;

    void Update()
    {
        if ( isActive && Time.time - _lastTime > 1.0f )
        {
            _lastTime = Time.time;

            if ( solarStore.Stock > 0 )
            {
                _model.Lifetime -= 1;
                solarStore.Stock -= 1;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void Setup( SolarModel model )
    {
        _model = model;
        
        solarStore = GetComponent<StoreComponent>();
        solarStore.Name = _model.Name;
        solarStore.MaxStock = _model.Lifetime;
        solarStore.Stock = _model.Lifetime;
        solarStore.Property = _model.Radius + "";

        Messenger.Dispatch( PlanetMessage.CREATE_PLANET, new PlanetMessage( _model.Planets, _planetsContainer ) );

        isActive = true;
    }

    private void OnDestroy()
    {
        //_model = null;
        //solarStore = null;
    }
}
