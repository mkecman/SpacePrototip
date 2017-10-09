using UnityEngine;
using System.Collections;
using System;
using UniRx;

public class AtomComponent : AbstractView
{
    public AtomStockUpgradeComponent UpgradeComponent;
    public StoreComponent StoreComponent;

    private AtomModel _model;

    public void UpdateModel( AtomModel model )
    {
        _model = model;

        StoreComponent.Name = _model.Symbol;
        StoreComponent.Property = "x" + _model.AtomicWeight.ToString( "F2" );
        
        UpgradeComponent.UpdateModel( _model );

        _model.rStock.Subscribe( stock => StoreComponent.Stock = stock );
        _model.rMaxStock.Subscribe( maxStock => StoreComponent.MaxStock = maxStock );
    }
}
