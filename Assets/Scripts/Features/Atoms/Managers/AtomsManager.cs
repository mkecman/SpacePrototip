using UnityEngine;
using System.Collections.Generic;
using System;

public class AtomsManager : AbstractController
{
    public GameObject atomPrefab;
    public Transform atomsContainer;

    private Dictionary<int, GameObject> gameObjects = new Dictionary<int, GameObject>();
    private int nextLevelMaxStock = 10;

    void Start()
    {
        Messenger.Listen( AtomMessage.SETUP_ATOMS, SetupAtoms );
        Messenger.Listen( AtomMessage.GENERATE_ATOM, GenerateAtom );
        Messenger.Listen( AtomMessage.ATOM_STOCK_CHANGED, AtomStockChanged );
        Messenger.Listen( AtomMessage.ATOM_STOCK_UPGRADE, AtomStockUpgrade );
        Messenger.Listen( AtomMessage.DEDUCT_ATOMS_WORTH_SC, DeductAtomsWorthSC );
        
    }

    private void SetupAtoms( AbstractMessage message )
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
            if( gameModel.User.Atoms[ atomicNumber ] == null )
            {
                userAtom = new AtomModel();
                userAtom.Stock = 0;
                userAtom.MaxStock = 1;
                gameModel.User.Atoms[ atomicNumber ] = userAtom;
            }
            else
            {
                userAtom = gameModel.User.Atoms[ atomicNumber ];
            }
            userAtom.UpgradeLevel = 1;

            atomStore.MaxStock = userAtom.MaxStock;
            atomStore.Stock = userAtom.Stock;
            
            gameObjects.Add( atomicNumber, go );

            AtomStockUpgradeComponent stockUpgradeComp = go.GetComponentInChildren<AtomStockUpgradeComponent>();
            int nextLevelSC = getNextUpgradePrice( userAtom );
            stockUpgradeComp.Setup( atomicNumber, userAtom.MaxStock + 10, nextLevelSC );
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
            getStore( randomAtomIndex ).Stock = gameModel.User.Atoms[ randomAtomIndex ].Stock;

            Messenger.Dispatch( AtomMessage.ATOM_STOCK_UPDATED, new AtomMessage( randomAtomIndex, 1 ) );
        }
    }

    private void AtomStockChanged( AbstractMessage message )
    {
        AtomMessage data = message as AtomMessage;
        AtomModel atom = gameModel.User.Atoms[ data.AtomicNumber ];
        int atomStock = atom.Stock;
        int maxAtomStock = atom.MaxStock;
        
        if( atomStock + data.Delta <= maxAtomStock )
        {
            atom.Stock += data.Delta;
            getStore( data.AtomicNumber ).Stock = atom.Stock;

            Messenger.Dispatch( AtomMessage.ATOM_STOCK_UPDATED, data );
        }
    }

    private void AtomStockUpgrade( AbstractMessage msg )
    {
        AtomMessage message = msg as AtomMessage;
        int atomicNumber = message.AtomicNumber;
        AtomModel atomModel = gameModel.User.Atoms[ atomicNumber ];

        int currentPrice = getNextUpgradePrice( atomModel );
        if( gameModel.User.SC < currentPrice )
        {
            Debug.Log( "NOT ENOUGH ATOMIC MASS TO UPGRADE!" );
            return;
        }

        DeductAtomsWorthSC( new AtomMessage( 0, 0, currentPrice ) );

        atomModel.UpgradeLevel += 1;
        atomModel.MaxStock += nextLevelMaxStock;

        int nextLevelPrice = getNextUpgradePrice( atomModel );
        AtomStockUpgradeComponent upgradeComp = gameObjects[ message.AtomicNumber ].GetComponentInChildren<AtomStockUpgradeComponent>();
        upgradeComp.Setup( message.AtomicNumber, atomModel.MaxStock + nextLevelMaxStock, nextLevelPrice );

        getStore( message.AtomicNumber ).MaxStock = atomModel.MaxStock;
    }

    private void DeductAtomsWorthSC( AbstractMessage msg )
    {
        AtomMessage message = msg as AtomMessage;
        float _sc = message.SC;
        Dictionary<int, AtomModel> collectedAtoms = new Dictionary<int, AtomModel>();
        Dictionary<int, AtomMessage> atomsMessages = new Dictionary<int, AtomMessage>();
        List<int> atomsList = new List<int>();

        foreach( AtomModel atomModel in gameModel.User.Atoms )
        {
            if( atomModel.Stock > 0 )
            {
                AtomModel atom = new AtomModel();
                atom.AtomicNumber = atomModel.AtomicNumber;
                atom.AtomicWeight = atomModel.AtomicWeight;
                atom.Stock = atomModel.Stock;
                collectedAtoms.Add( atomModel.AtomicNumber, atom );
                atomsList.Add( atomModel.AtomicNumber );
                atomsMessages.Add( atomModel.AtomicNumber, new AtomMessage( atomModel.AtomicNumber, 0 ) );
            }
        }

        atomsList.Sort();
        atomsList.Reverse();

        while( _sc > 0 )
        {
            for( int i = 0; i < atomsList.Count; i++ )
            {
                AtomModel atomModel = collectedAtoms[ atomsList[ i ] ];
                if( atomModel.Stock > 0 )
                {
                    if( _sc - atomModel.AtomicWeight >= 0 )
                    {
                        _sc -= atomModel.AtomicWeight;
                        atomModel.Stock -= 1;
                        atomsMessages[ atomModel.AtomicNumber ].Delta -= 1;
                    }
                    else
                    {
                        _sc = 0;
                        break;
                    }
                }
            }
        }

        foreach( KeyValuePair<int, AtomMessage> atomMessage in atomsMessages )
        {
            AtomStockChanged( atomMessage.Value );
        }
    }

    private StoreComponent getStore( int atomicNumber )
    {
        return gameObjects[ atomicNumber ].GetComponentInChildren<StoreComponent>();
    }

    private int getNextUpgradePrice( AtomModel model )
    {
        return (int)Math.Round( model.AtomicWeight * model.MaxStock );
    }

}
