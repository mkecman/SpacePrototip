using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    public GameConfig Config;
    private UserConfig rawUser;
    private AtomConfig rawAtoms;
    private RecipeConfig rawRecipes;
    
    public UserModel User;
    public List<AtomModel> Atoms;
    public Dictionary<string, AtomModel> AtomsBySymbol;
    public List<RecipeModel> Recipes;
    
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
        Config = new GameConfig();
        
        rawAtoms = new AtomConfig();
        rawAtoms.Load();
        Atoms = rawAtoms.Data;
        AtomsBySymbol = rawAtoms.AtomsBySymbol;
        
        rawUser = new UserConfig();
        rawUser.Load();
        User = rawUser.Data;

        rawRecipes = new RecipeConfig();
        rawRecipes.Load( AtomsBySymbol );
        Recipes = rawRecipes.Data;

        Messenger.Dispatch(GameMessage.MODEL_LOADED);
    }

    public void SaveUser()
    {
        rawUser.Save();
    }
   
}
