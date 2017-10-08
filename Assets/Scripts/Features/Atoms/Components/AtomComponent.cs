using UnityEngine;
using System.Collections;

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
        UpdateView();
        UpgradeComponent.UpdateModel( _model );
    }

    public void UpdateView()
    {
        StoreComponent.Stock = _model.Stock;
        StoreComponent.MaxStock = _model.MaxStock;
    }

    public void UpdateUpgradeView()
    {
        UpgradeComponent.UpdateView();
    }
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
