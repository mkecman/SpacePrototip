using UnityEngine;

public class SCController : AbstractController
{    

    public void Start()
    {
        //Messenger.Listen( "AtomStockChanged", CalculateSCBasedOnConfigData );
    }

    public void CalculateSCBasedOnConfigData( object data )
    {
        float SC = 0;
        int atomsLength = model.User.Data.AtomsStock.Length;
        
        for( int atomIndex = 0; atomIndex < atomsLength; atomIndex++ )
        {
            float atomicWeight = model.Atoms.Data[ atomIndex ].AtomicWeight;
            int stock = model.User.Data.AtomsStock[ atomIndex ];
            SC += stock * atomicWeight;
        }

        model.User.Data.SC = SC;
    }
}
