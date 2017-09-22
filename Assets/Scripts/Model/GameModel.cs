using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    private UserConfig rawUser;
    private AtomConfig rawAtoms;
    
    public UserModel User;

    public AtomModel[] Atoms;
    public int atomsCount = 0;

    public float minSC = 8;
    public float maxSC = 1000;

    private static GameModel gameModel;

    public static GameModel instance
    {
        get
        {
            if( gameModel == null )
            {
                gameModel = new GameModel();
            }

            return gameModel;
        }
    }

    public void Init()
    {
        rawAtoms = new AtomConfig();
        rawAtoms.Load();
        Atoms = rawAtoms.Data;
        atomsCount = Atoms.Length-1;
        
        rawUser = new UserConfig();
        //rawUser.Load();
        User = rawUser.getUser( Atoms );
        User = rawUser.Data;

        Messenger.Dispatch(GameMessage.MODEL_LOADED);
    }

    public void SaveUser()
    {
        rawUser.Save();
    }
   
}
