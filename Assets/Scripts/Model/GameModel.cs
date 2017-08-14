using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    public UserConfig User = new UserConfig();
    public AtomConfig Atoms = new AtomConfig();
    public int atomsCount = 0;

    private static GameModel gameModel;

    public static GameModel instance
    {
        get
        {
            if( gameModel == null )
            {
                gameModel = new GameModel();
                gameModel.Init();
            }

            return gameModel;
        }
    }

    void Init()
    {
        atomsCount = Atoms.Data.Length;
    }
    
    public AtomModel getAtomByAtomicWeight( int weight )
    {
        if( weight > 0 && weight <= instance.atomsCount )
            return instance.Atoms.Data[ weight - 1 ];
        else
            return null;
    }

}
