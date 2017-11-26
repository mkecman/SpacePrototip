using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameModel
{
    private UserConfig rawUser;
    private AtomConfig rawAtoms;
    private RecipeConfig rawRecipes;
    private PlanetaryEnvironmentConfig rawPlanetaryEnvironments;
    
    public GameConfig Config;
    public UserModel User;
    public ReactiveCollection<AtomModel> Atoms;
    public Dictionary<string, AtomModel> AtomsBySymbol;
    public List<RecipeModel> Recipes;
    public AtomsModelManager AMM;
    public Dictionary<string, PlanetaryEnvironmentModel> PlanetaryEnvironments;


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
        Config.Load();
        
        rawAtoms = new AtomConfig();
        rawAtoms.Load();
        Atoms = rawAtoms.atoms;
        AtomsBySymbol = rawAtoms.AtomsBySymbol;
        
        rawUser = new UserConfig();
        rawUser.Load();
        User = rawUser.userModel;

        rawRecipes = new RecipeConfig();
        rawRecipes.Load( AtomsBySymbol );
        Recipes = rawRecipes.Data;

        AMM = new AtomsModelManager();
        AMM.Setup( this );

        rawPlanetaryEnvironments = new PlanetaryEnvironmentConfig();
        rawPlanetaryEnvironments.Load();
        PlanetaryEnvironments = rawPlanetaryEnvironments.rxBySymbol;

        Messenger.Dispatch(GameMessage.MODEL_LOADED);
    }

    public void SaveUser()
    {
        rawUser.Save();
    }
   
}
