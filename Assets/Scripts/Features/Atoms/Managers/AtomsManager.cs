﻿using UnityEngine;
using System.Collections.Generic;

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
        int randomAtomIndex = Random.Range( 1, gameModel.User.AtomsUnlocked +1 );
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
            atomStore = Instantiate( atomPrefab, atomsContainer ).GetComponent<StoreComponent>();
            atomStore.Name = atomModel.Symbol;
            atomStore.Property = atomModel.AtomicNumber + "";
            atomStore.MaxStock = gameModel.User.Atoms[ atomicNumber ].MaxStock;
            atomStore.Stock = gameModel.User.Atoms[ atomicNumber ].Stock;
            
            atomStores.Add( atomicNumber, atomStore );
        }
    }


}
