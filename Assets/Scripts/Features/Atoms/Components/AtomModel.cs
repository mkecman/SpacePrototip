using System;
using UnityEngine;

[Serializable]
public class AtomModel
{
    public string Name;
    public string Symbol;
    public int AtomicNumber;
    public float AtomicWeight;
    public string HexColor;
    public string GroupBlock;
    public int Stock = 0;
    public int MaxStock = 10;
    public int MaxStockUpgradePrice = 1;
    public int MaxStockNextLevel = 20;
    public float HarvestRate = .1f;
    public int HarvestRateUpgradePrice;

    public void calculateMaxStockUpgradePrice()
    {
        MaxStockUpgradePrice = (int)Math.Round( AtomicWeight * Mathf.Pow( 1.049f, MaxStock ) );
    }

    public void calculateHarvestRateUpgradePrice()
    {
        HarvestRateUpgradePrice = (int)( Mathf.Pow( 10f, HarvestRate ) * AtomicWeight );
    }
    
}
