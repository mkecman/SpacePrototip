using UnityEngine;
using System.Collections.Generic;

public class AtomsController : AbstractController
{
    public GameObject atomPrefab;
    public GameObject atomsContainer;

    private List<StoreComponent> atomViews = new List<StoreComponent>();
    
    void Start()
    {
        Messenger.Listen( AtomMessage.SETUP_ATOMS, SetupAtoms );
        Messenger.Listen( AtomMessage.GENERATE_ATOM, GenerateAtom );
    }

    private void GenerateAtom( AbstractMessage message )
    {
        int randomAtomIndex = Random.Range( 0, gameModel.User.Data.AtomsUnlocked );
        int maxStock = gameModel.User.Data.AtomsMax[ randomAtomIndex ];
        int newStock = gameModel.User.Data.AtomsStock[ randomAtomIndex ] + 1;

        if( newStock <= maxStock )
        {
            gameModel.User.Data.AtomsStock[ randomAtomIndex ] = newStock;
            Messenger.Dispatch( "AtomStockChanged" );
        }

        atomViews[ randomAtomIndex ].Stock = gameModel.User.Data.AtomsStock[ randomAtomIndex ];
    }

    public void SetupAtoms( AbstractMessage message )
    {
        AtomModel atomModel;
        GameObject atomView;
        Transform acTransform = atomsContainer.transform;

        for( int index = 0; index < gameModel.User.Data.AtomsUnlocked; index++ )
        {
            atomModel = gameModel.getAtomByAtomicWeight( index + 1 );
            atomModel.MaxStock = gameModel.User.Data.AtomsMax[ index ];
            atomModel.Stock = gameModel.User.Data.AtomsStock[ index ];

            atomView = Instantiate( atomPrefab, acTransform );
            atomViews.Add( atomView.GetComponent<StoreComponent>() );

            atomViews[ index ].Name = atomModel.Name;
            atomViews[ index ].Property = atomModel.AtomicNumber + "";
            atomViews[ index ].MaxStock = atomModel.MaxStock;
            atomViews[ index ].Stock = atomModel.Stock;           
            
        }
    }


}
