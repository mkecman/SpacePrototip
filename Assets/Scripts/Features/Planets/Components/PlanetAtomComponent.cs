using UnityEngine;
using System.Collections;
using UniRx;
using System;

public class PlanetAtomComponent : AbstractView
{
    public StoreComponent PlanetAtomStore;
    private AtomModel _model;

    public void UpdateModel( AtomModel model )
    {
        _model = model;
        
        PlanetAtomStore.ID = _model.AtomicNumber.ToString();
        PlanetAtomStore.Name = _model.Symbol;
        PlanetAtomStore.MaxStock = _model.Stock;
        PlanetAtomStore.Stock = _model.Stock;

        PlanetAtomUpgradeComponent upgradeComp = gameObject.GetComponent<PlanetAtomUpgradeComponent>();
        upgradeComp.UpdateModel( _model );

        HarvesterComponent harvester = gameObject.GetComponent<HarvesterComponent>();
        harvester.UpdateModel( _model );

        _model.rStock.Subscribe( stock => PlanetAtomStore.Stock = stock ).AddTo( this );
    }

    void OnDestroy()
    {
        _model = null;
    }


}
