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
        PlanetAtomStore.MaxStock = _model.MaxStock;
        PlanetAtomStore.Stock = _model.Stock;

        PlanetAtomUpgradeComponent upgradeComp = gameObject.GetComponent<PlanetAtomUpgradeComponent>();
        upgradeComp.UpdateModel( _model );

        HarvesterComponent harvester = gameObject.GetComponent<HarvesterComponent>();
        harvester.UpdateModel( _model );

        _model.rStock.Subscribe( stock => { PlanetAtomStore.Stock = stock; PlanetAtomStore.Property = stock + "(" + (stock * _model.AtomicWeight).ToString( "F0" ) + "AM)"; } ).AddTo( this );
        _model.rMaxStock.Subscribe( maxStock => PlanetAtomStore.MaxStock = maxStock ).AddTo( this );
    }

    void OnDestroy()
    {
        _model = null;
    }


}
