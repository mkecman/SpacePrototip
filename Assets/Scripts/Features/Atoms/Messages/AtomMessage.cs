using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class AtomMessage : AbstractMessage
{
    public static string SETUP_ATOMS = "SetupAtoms";
    public static string GENERATE_ATOM = "GenerateAtom";
    public static string ATOM_STOCK_CHANGED = "AtomStockChanged";
    public static string ATOM_STOCK_UPDATED = "AtomStockUpdated";


    private int _atomicNumber;
    private int _delta;

    public AtomMessage( int atomicNumber, int delta )
    {
        _atomicNumber = atomicNumber;
        _delta = delta;
    }

    public int AtomicNumber
    {
        get { return _atomicNumber; }
    }

    public int Delta
    {
        get { return _delta; }
    }

}
