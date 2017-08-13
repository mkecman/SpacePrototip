using UnityEngine;

public class SCController : AbstractController
{    

    void Start()
    {
        //Messenger.Listen( "AtomStockChanged", CalculateSCBasedOnConfigData );
    }

    public void CalculateSCBasedOnConfigData( object data )
    {
        float SC = 0;
        int atomsLength = gameModel.User.Data.AtomsStock.Length;
        
        for( int atomIndex = 0; atomIndex < atomsLength; atomIndex++ )
        {
            float atomicWeight = gameModel.Atoms.Data[ atomIndex ].AtomicWeight;
            int stock = gameModel.User.Data.AtomsStock[ atomIndex ];
            SC += stock * atomicWeight;
        }

        gameModel.User.Data.SC = SC;
    }
}
