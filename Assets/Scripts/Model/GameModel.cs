using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : MonoBehaviour
{
    public ConfigUser User = new ConfigUser();
    public ConfigAtoms Atoms = new ConfigAtoms();
    public int atomsCount = 0;
           
    public GameModel()
    {
        atomsCount = Atoms.Data.Length;
    }

    public AtomModel getAtomByAtomicWeight( int weight )
    {
        if( weight > 0 && weight <= atomsCount )
            return Atoms.Data[ weight - 1 ];
        else
            return null;
    }

}
