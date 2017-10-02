using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    private UserConfig rawUser;
    private AtomConfig rawAtoms;
    private RecipeConfig rawRecipes;
    
    public UserModel User;

    public AtomModel[] Atoms;
    public int atomsCount = 0;

    public float minSC = 8.0f;
    public float maxSC = 15000.0f;

    public List<RecipeModel> Recipes;

    public Color32 GreenColor = new Color32(0, 120, 20, 255);
    

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
        rawUser.Load();
        User = rawUser.Data;

        rawRecipes = new RecipeConfig();
        rawRecipes.Load( Atoms );
        Recipes = rawRecipes.Data;

        Messenger.Dispatch(GameMessage.MODEL_LOADED);
    }

    public void SaveUser()
    {
        rawUser.Save();
    }
   
}
