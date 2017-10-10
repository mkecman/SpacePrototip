using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class AtomMessage : AbstractMessage
{
    public static string SETUP_ATOMS = "SetupAtoms";
    public static string GENERATE_ATOM = "GenerateAtom";
    public static string ATOM_HARVEST = "AtomHarvest";
    public static string ATOM_STOCK_UPDATE = "AtomStockUpdate";
    public static string ATOM_STOCK_UPDATED = "AtomStockUpdated";
    public static string ATOM_STOCK_UPGRADE = "AtomStockUpgrade";
    public static string SPEND_ATOMS = "SpendAtoms";

    private int _atomicNumber;
    private int _delta;
    private float _SC;

    public AtomMessage( int atomicNumber, int delta, float SC = 0 )
    {
        _atomicNumber = atomicNumber;
        _delta = delta;
        _SC = SC;
    }

    public float SC
    {
        get { return _SC; }
    }
    
    public int AtomicNumber
    {
        get { return _atomicNumber; }
        set { _atomicNumber = value; }
    }

    public int Delta
    {
        get { return _delta; }
        set { _delta = value; }
    }

}
