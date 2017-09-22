using System;
using UnityEngine;

[Serializable]
public class AtomModel
{
    public string Name;
    public string Symbol;
    public int AtomicNumber;
    public float AtomicWeight;
    public int Stock = 0;
    public int MaxStock = 1;
    public int UpgradeLevel = 1;
}
