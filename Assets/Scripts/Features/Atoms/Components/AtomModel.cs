using UnityEngine;
using System.Collections;
using UniRx;
using System;

public class AtomModel
{
    public JSONAtomModel Model;

    public StringReactiveProperty rName = new StringReactiveProperty();
    public StringReactiveProperty rSymbol = new StringReactiveProperty();
    public IntReactiveProperty rAtomicNumber = new IntReactiveProperty( 0 );
    public FloatReactiveProperty rAtomicWeight = new FloatReactiveProperty( 0 );
    public StringReactiveProperty rHexColor = new StringReactiveProperty();
    public StringReactiveProperty rGroupBlock = new StringReactiveProperty();
    public IntReactiveProperty rStock = new IntReactiveProperty( 0 );
    public IntReactiveProperty rMaxStock = new IntReactiveProperty( 10 );
    public IntReactiveProperty rMaxStockUpgradePrice = new IntReactiveProperty( 1 );
    public IntReactiveProperty rMaxStockNextLevel = new IntReactiveProperty( 20 );
    public FloatReactiveProperty rHarvestRate = new FloatReactiveProperty(.1f);
    public IntReactiveProperty rHarvestRateUpgradePrice = new IntReactiveProperty( 1 );

    private IDisposable maxStockSubscriber;
    private IDisposable harvestRateSubscriber;

    public AtomModel( JSONAtomModel model )
    {
        maxStockSubscriber = rMaxStock.Subscribe( _ => MaxStockUpgradePrice = (int)( AtomicWeight * Mathf.Pow( 1.049f, MaxStock ) ) );
        harvestRateSubscriber = rHarvestRate.Subscribe( _ => HarvestRateUpgradePrice = (int)( Mathf.Pow( 10f, HarvestRate ) * AtomicWeight ) );

        fromJSON(model);
    }

    public void fromJSON( JSONAtomModel model )
    {
        Model = model;

        rName.Value = Model.Name;
        rSymbol.Value = Model.Symbol;
        rAtomicNumber.Value = Model.AtomicNumber;
        rAtomicWeight.Value = Model.AtomicWeight;
        rHexColor.Value = Model.HexColor;
        rGroupBlock.Value = Model.GroupBlock;
        rStock.Value = Model.Stock;
        rMaxStock.Value = Model.MaxStock;
        rMaxStockNextLevel.Value = Model.MaxStockNextLevel;
        rHarvestRate.Value = Model.HarvestRate;
    }

    public JSONAtomModel toJSON()
    {
        Model.Name = rName.Value;
        Model.Symbol = rSymbol.Value;
        Model.AtomicNumber = rAtomicNumber.Value;
        Model.AtomicWeight = rAtomicWeight.Value;
        Model.HexColor = rHexColor.Value;
        Model.GroupBlock = rGroupBlock.Value;
        Model.Stock = rStock.Value;
        Model.MaxStock = rMaxStock.Value;
        Model.MaxStockNextLevel = rMaxStockNextLevel.Value;
        Model.HarvestRate = rHarvestRate.Value;

        return Model;
    }

    public void OnDestroy()
    {
        maxStockSubscriber.Dispose();
        maxStockSubscriber = null;
        harvestRateSubscriber.Dispose();
        harvestRateSubscriber = null;
    }

    public string Name
    {
        set { rName.Value = value; }
        get { return rName.Value; }
    }

    public string Symbol
    {
        set { rSymbol.Value = value; }
        get { return rSymbol.Value; }
    }

    public int AtomicNumber
    {
        set { rAtomicNumber.Value = value; }
        get { return rAtomicNumber.Value; }
    }

    public float AtomicWeight
    {
        set { rAtomicWeight.Value = value; }
        get { return rAtomicWeight.Value; }
    }

    public string HexColor
    {
        set { rHexColor.Value = value; }
        get { return rHexColor.Value; }
    }

    public int Stock
    {
        set { rStock.Value = value; }
        get { return rStock.Value; }
    }

    public int MaxStock
    {
        set { rMaxStock.Value = value; }
        get { return rMaxStock.Value; }
    }

    public int MaxStockUpgradePrice
    {
        set { rMaxStockUpgradePrice.Value = value; }
        get { return rMaxStockUpgradePrice.Value; }
    }

    public int MaxStockNextLevel
    {
        set { rMaxStockNextLevel.Value = value; }
        get { return rMaxStockNextLevel.Value; }
    }

    public float HarvestRate
    {
        set { rHarvestRate.Value = value; }
        get { return rHarvestRate.Value; }
    }

    public int HarvestRateUpgradePrice
    {
        set { rHarvestRateUpgradePrice.Value = value; }
        get { return rHarvestRateUpgradePrice.Value; }
    }
}
