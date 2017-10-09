using System;
using UnityEngine;

[Serializable]
public class JSONAtomModel
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
    public int HarvestRateUpgradePrice = 1;
}
