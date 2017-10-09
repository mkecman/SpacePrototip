using UnityEngine;
using System.Collections;
using UniRx;

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

    public AtomModel( JSONAtomModel model )
    {
        Model = model;

        rMaxStock.Subscribe( _ => MaxStockUpgradePrice = (int)( AtomicWeight * Mathf.Pow( 1.049f, MaxStock ) ) );
        rHarvestRate.Subscribe( _ => HarvestRateUpgradePrice = (int)( Mathf.Pow( 10f, HarvestRate ) * AtomicWeight ) );

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

    public string Name
    {
        set { Model.Name = value; rName.Value = value; }
        get { return rName.Value; }
    }

    public string Symbol
    {
        set { Model.Symbol = value; rSymbol.Value = value; }
        get { return rSymbol.Value; }
    }

    public int AtomicNumber
    {
        set { Model.AtomicNumber = value; rAtomicNumber.Value = value; }
        get { return rAtomicNumber.Value; }
    }

    public float AtomicWeight
    {
        set { Model.AtomicWeight = value; rAtomicWeight.Value = value; }
        get { return rAtomicWeight.Value; }
    }

    public string HexColor
    {
        set { Model.HexColor = value; rHexColor.Value = value; }
        get { return rHexColor.Value; }
    }

    public int Stock
    {
        set { Model.Stock = value; rStock.Value = value; }
        get { return rStock.Value; }
    }

    public int MaxStock
    {
        set { Model.MaxStock = value; rMaxStock.Value = value; }
        get { return rMaxStock.Value; }
    }

    public int MaxStockUpgradePrice
    {
        set { Model.MaxStockUpgradePrice = value; rMaxStockUpgradePrice.Value = value; }
        get { return rMaxStockUpgradePrice.Value; }
    }

    public int MaxStockNextLevel
    {
        set { Model.MaxStockNextLevel = value; rMaxStockNextLevel.Value = value; }
        get { return rMaxStockNextLevel.Value; }
    }

    public float HarvestRate
    {
        set { Model.HarvestRate = value; rHarvestRate.Value = value; }
        get { return rHarvestRate.Value; }
    }

    public int HarvestRateUpgradePrice
    {
        set { Model.HarvestRateUpgradePrice = value; rHarvestRateUpgradePrice.Value = value; }
        get { return rHarvestRateUpgradePrice.Value; }
    }
}
