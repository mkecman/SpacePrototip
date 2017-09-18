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
    public static string ATOM_STOCK_UPGRADE = "AtomStockUpgrade";


    private int _atomicNumber;
    private int _delta;
    private GameObject _dispatcherGO;

    public AtomMessage( int atomicNumber, int delta, GameObject dispatcherGO = null )
    {
        _atomicNumber = atomicNumber;
        _delta = delta;
        _dispatcherGO = dispatcherGO;
    }

    public GameObject dispatcherGO
    {
        get { return _dispatcherGO; }
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
