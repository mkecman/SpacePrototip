using UnityEngine;
using System.Collections;

public class HarvesterMessage : AbstractMessage
{

    public static string HARVESTER_UPGRADED = "HarversterUpgraded";
    
    private int _atomicNumber;
    private int _delta;

    public HarvesterMessage( int atomicNumber, int delta )
    {
        _atomicNumber = atomicNumber;
        _delta = delta;
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
