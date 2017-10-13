using UnityEngine;

public class RecipesManager : AbstractController
{
    void Start()
    {
        Messenger.Listen( RecipeMessage.CRAFT_COMPOUND_REQUEST, handleCraftCompoundRequest );
    }

    private void handleCraftCompoundRequest( AbstractMessage msg )
    {
        RecipeMessage recipeMessage = msg as RecipeMessage;
        RecipeModel model = recipeMessage.Model;
        
        for( int j = 0; j < model.FormulaAtomsList.Count; j++ )
        {
            gameModel.AMM.UpdateAtomStock( model.FormulaAtomsList[ j ].AtomicNumber, - recipeMessage.Amount * model.FormulaAtomsList[ j ].Amount );
        }
        
        Messenger.Dispatch( HCMessage.UPDATE_REQUEST, new HCMessage( Mathf.RoundToInt( model.MolecularMass * model.ExchangeRate * recipeMessage.Amount ) ) );
    }
    
}
