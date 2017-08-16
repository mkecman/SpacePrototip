using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    private UserConfig rawUser = new UserConfig();
    private AtomConfig rawAtoms = new AtomConfig();
    public int atomsCount = 0;

    public UserModel User;
    public AtomModel[] Atoms;

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
        Atoms = rawAtoms.Data;
        atomsCount = Atoms.Length;
        User = rawUser.getUser( Atoms );
    }
    
    public AtomModel getAtomByAtomicNumber( int atomicNumber )
    {
        if( atomicNumber > 0 && atomicNumber < instance.atomsCount )
            return instance.Atoms[ atomicNumber ];
        else
            return null;
    }

}
