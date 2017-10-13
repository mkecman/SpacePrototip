using UnityEngine;
using System.Collections;
using UniRx;
using System;

public class AtomModel
{
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
    public FloatReactiveProperty rHarvestRate = new FloatReactiveProperty(1f);
    public IntReactiveProperty rHarvestRateUpgradePrice = new IntReactiveProperty( 1 );

    private IDisposable maxStockSubscriber;
    private IDisposable harvestRateSubscriber;

    public AtomModel( JSONAtomModel model )
    {
        maxStockSubscriber = rMaxStock.Subscribe( _ => MaxStockUpgradePrice = (int)( AtomicWeight * Mathf.Pow( 1.049f, MaxStock ) ) );
        harvestRateSubscriber = rHarvestRate.Subscribe( _ => HarvestRateUpgradePrice = (int)( Mathf.Pow( 10f, HarvestRate ) * AtomicWeight ) );

        fromJSON(model);
    }

    public AtomModel Copy()
    {
        return new AtomModel( this.toJSON() );
    }

    public void fromJSON( JSONAtomModel model )
    {
        rName.Value = model.Name;
        rSymbol.Value = model.Symbol;
        rAtomicNumber.Value = model.AtomicNumber;
        rAtomicWeight.Value = model.AtomicWeight;
        rHexColor.Value = model.HexColor;
        rGroupBlock.Value = model.GroupBlock;
        rStock.Value = model.Stock;
        rMaxStock.Value = model.MaxStock;
        rMaxStockNextLevel.Value = model.MaxStockNextLevel;
        rHarvestRate.Value = model.HarvestRate;
    }

    public JSONAtomModel toJSON()
    {
        JSONAtomModel model = new JSONAtomModel();

        model.Name = rName.Value;
        model.Symbol = rSymbol.Value;
        model.AtomicNumber = rAtomicNumber.Value;
        model.AtomicWeight = rAtomicWeight.Value;
        model.HexColor = rHexColor.Value;
        model.GroupBlock = rGroupBlock.Value;
        model.Stock = rStock.Value;
        model.MaxStock = rMaxStock.Value;
        model.MaxStockNextLevel = rMaxStockNextLevel.Value;
        model.HarvestRate = rHarvestRate.Value;

        return model;
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
