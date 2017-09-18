using UnityEngine;
using System.Collections.Generic;
using System;

public class AtomsManager : AbstractController
{
    public GameObject atomPrefab;
    public Transform atomsContainer;

    private Dictionary<int, StoreComponent> atomStores = new Dictionary<int, StoreComponent>();
    
    void Start()
    {
        Messenger.Listen( AtomMessage.SETUP_ATOMS, SetupAtoms );
        Messenger.Listen( AtomMessage.GENERATE_ATOM, GenerateAtom );
        Messenger.Listen( AtomMessage.ATOM_STOCK_CHANGED, AtomStockChanged );
        Messenger.Listen( AtomMessage.ATOM_STOCK_UPGRADE, AtomStockUpgrade );
    }

    private void AtomStockUpgrade( AbstractMessage msg )
    {
        int nextLevelMaxStock = 10;


        AtomMessage message = msg as AtomMessage;
        int atomicNumber = message.AtomicNumber;
        AtomModel atomModel = gameModel.User.Atoms[ atomicNumber ];
        
        int upgradeLevel = atomModel.UpgradeLevel;
        int nextLevelPrice = (int)Math.Round( upgradeLevel * atomModel.AtomicWeight * atomModel.MaxStock );
        if( gameModel.User.SC < nextLevelPrice )
        {
            Debug.Log( "NOT ENOUGH ATOMIC MASS TO UPGRADE!" );
            return;
        }

        atomModel.UpgradeLevel += 1;
        atomModel.MaxStock += nextLevelMaxStock;

        gameModel.User.SC -= nextLevelPrice;
        int nextLevelSC = (int)Math.Round( atomModel.UpgradeLevel * atomModel.AtomicWeight * atomModel.MaxStock );

        AtomStockUpgradeComponent upgradeComp = message.dispatcherGO.GetComponent<AtomStockUpgradeComponent>();
        upgradeComp.Setup( message.AtomicNumber, atomModel.MaxStock + nextLevelMaxStock, nextLevelSC );

        //TODO: update SC properly, update Atom view

    }

    private void AtomStockChanged( AbstractMessage message )
    {
        AtomMessage data = message as AtomMessage;
        int atomStock = gameModel.User.Atoms[ data.AtomicNumber ].Stock;
        int maxAtomStock = gameModel.User.Atoms[ data.AtomicNumber ].MaxStock;
        if( atomStock + data.Delta <= maxAtomStock )
        {
            gameModel.User.Atoms[ data.AtomicNumber ].Stock += data.Delta;
            atomStores[ data.AtomicNumber ].Stock = gameModel.User.Atoms[ data.AtomicNumber ].Stock;
            Messenger.Dispatch( AtomMessage.ATOM_STOCK_UPDATED, data );
        }
    }

    private void GenerateAtom( AbstractMessage message )
    {
        int randomAtomIndex = UnityEngine.Random.Range( 1, gameModel.User.AtomsUnlocked +1 );
        int maxStock = gameModel.User.Atoms[ randomAtomIndex ].MaxStock;
        int newStock = gameModel.User.Atoms[ randomAtomIndex ].Stock + 1;

        if( newStock <= maxStock )
        {
            gameModel.User.Atoms[ randomAtomIndex ].Stock = newStock;
            atomStores[ randomAtomIndex ].Stock = gameModel.User.Atoms[ randomAtomIndex ].Stock;

            Messenger.Dispatch( AtomMessage.ATOM_STOCK_UPDATED, new AtomMessage( randomAtomIndex, 1 ) );
        }
    }

    public void SetupAtoms( AbstractMessage message )
    {
        AtomModel atomModel;
        StoreComponent atomStore;

        for( int atomicNumber = 1; atomicNumber <= gameModel.User.AtomsUnlocked; atomicNumber++ )
        {
            atomModel = gameModel.getAtomByAtomicNumber( atomicNumber );
            GameObject go = Instantiate( atomPrefab, atomsContainer );
            atomStore = go.GetComponentInChildren<StoreComponent>();
            atomStore.Name = atomModel.Symbol;
            atomStore.Property = atomModel.AtomicWeight.ToString( "F2" );

            AtomModel userAtom;
            if ( gameModel.User.Atoms[ atomicNumber ] == null )
            {
                userAtom = new AtomModel();
                userAtom.Stock = 0;
                userAtom.MaxStock = 100;
                gameModel.User.Atoms[ atomicNumber ] = userAtom;
            }
            else
            {
                userAtom = gameModel.User.Atoms[atomicNumber];
            }
            userAtom.UpgradeLevel = 1;
            
            atomStore.MaxStock = userAtom.MaxStock;
            atomStore.Stock = userAtom.Stock;
            

            atomStores.Add( atomicNumber, atomStore );

            AtomStockUpgradeComponent stockUpgradeComp = go.GetComponentInChildren<AtomStockUpgradeComponent>();
            stockUpgradeComp.Setup( atomicNumber, 1, 1 );
        }
    }
    
}
