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

        float collectedSC = 0;
        for( int j = 0; j < model.FormulaAtomsList.Count; j++ )
        {
            collectedSC += recipeMessage.Amount * model.FormulaAtomsList[ j ].Amount * gameModel.Atoms[ model.FormulaAtomsList[ j ].AtomicNumber].AtomicWeight;
        }

        Messenger.Dispatch( AtomMessage.SPEND_ATOMS, new AtomMessage( 0, 0, collectedSC ) );
        Messenger.Dispatch( HCMessage.UPDATE_REQUEST, new HCMessage( Mathf.RoundToInt( model.MolecularMass * model.ExchangeRate * recipeMessage.Amount ) ) );
    }
    
}
