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
    public int UpgradeLevel = 1;
}
