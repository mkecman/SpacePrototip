using UnityEngine;
using System.Collections.Generic;

public class AtomsController : AbstractController
{
    public GameObject atomPrefab;
    public GameObject atomsContainer;

    private List<StoreView> atomViews = new List<StoreView>();
    
    public void Start()
    {
        Messenger.Listen( AtomMessage.SETUP_ATOMS, SetupAtoms );
        Messenger.Listen( AtomMessage.GENERATE_ATOM, GenerateAtom );
    }

    private void GenerateAtom( AbstractMessage message )
    {
        int randomAtomIndex = Random.Range( 0, model.User.Data.AtomsUnlocked );
        int maxStock = model.User.Data.AtomsMax[ randomAtomIndex ];
        int newStock = model.User.Data.AtomsStock[ randomAtomIndex ] + 1;

        if( newStock <= maxStock )
        {
            model.User.Data.AtomsStock[ randomAtomIndex ] = newStock;
            Messenger.Dispatch( "AtomStockChanged" );
        }

        atomViews[ randomAtomIndex ].Stock = model.User.Data.AtomsStock[ randomAtomIndex ];
    }

    public void SetupAtoms( AbstractMessage message )
    {
        AtomModel atomModel;
        GameObject atomView;
        Transform acTransform = atomsContainer.transform;

        Debug.Log( model.User.Data.AtomsUnlocked );

        for( int index = 0; index < model.User.Data.AtomsUnlocked; index++ )
        {
            atomModel = model.getAtomByAtomicWeight( index + 1 );
            atomModel.MaxStock = model.User.Data.AtomsMax[ index ];
            atomModel.Stock = model.User.Data.AtomsStock[ index ];

            atomView = Instantiate( atomPrefab, acTransform );
            atomViews.Add( atomView.GetComponent<StoreView>() );

            atomViews[ index ].Name = atomModel.Name;
            atomViews[ index ].Property = atomModel.AtomicNumber + "";
            atomViews[ index ].MaxStock = atomModel.MaxStock;
            atomViews[ index ].Stock = atomModel.Stock;           
            
        }
    }


}
