using UnityEngine;
using System.Collections;
using UniRx;
using System;

public class PlanetAtomComponent : AbstractView
{
    private AtomModel _model;

    public void UpdateModel( AtomModel model )
    {
        _model = model;
        
        StoreComponent planetAtomStore = gameObject.GetComponent<StoreComponent>();
        planetAtomStore.ID = _model.AtomicNumber.ToString();
        planetAtomStore.Name = _model.Symbol;
        planetAtomStore.MaxStock = _model.Stock;
        planetAtomStore.Stock = _model.Stock;

        PlanetAtomUpgradeComponent upgradeComp = gameObject.GetComponent<PlanetAtomUpgradeComponent>();
        upgradeComp.UpdateModel( _model );

        HarvesterComponent harvester = gameObject.GetComponent<HarvesterComponent>();
        harvester.UpdateModel( _model );

        _model.rStock.Subscribe( stock => planetAtomStore.Stock = stock ).AddTo( this );
    }

    void OnDestroy()
    {
        _model = null;
    }


}
