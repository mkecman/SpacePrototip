using UnityEngine;
using System.Collections.Generic;
using System;

public class AtomsManager : AbstractController
{
    private AtomsModelManager AMM = new AtomsModelManager();

    public GameObject atomPrefab;
    public Transform atomsContainer;

    private Dictionary<int, AtomComponent> _atoms = new Dictionary<int, AtomComponent>();

    void Start()
    {
        Messenger.Listen( AtomMessage.SETUP_ATOMS, SetupAtoms );
        Messenger.Listen( AtomMessage.GENERATE_ATOM, GenerateAtom );
        Messenger.Listen( AtomMessage.ATOM_HARVEST, AtomHarvested );
        Messenger.Listen( AtomMessage.ATOM_STOCK_UPGRADE, AtomStockUpgrade );
        Messenger.Listen( AtomMessage.DEDUCT_ATOMS_WORTH_SC, DeductAtomsWorthSC );
        Messenger.Listen( AtomMessage.ATOM_STOCK_UPDATE, handleAtomStockUpdate );
    }
    
    private void SetupAtoms( AbstractMessage message )
    {
        AMM.Setup( gameModel.Atoms, gameModel.User );
        for( int atomicNumber = 1; atomicNumber < gameModel.User.Atoms.Count; atomicNumber++ )
        {
            CreateAtom( gameModel.User.Atoms[ atomicNumber ] );
        }
    }

    private void CreateAtom( AtomModel atomModel )
    {
        GameObject go = Instantiate( atomPrefab, atomsContainer );
        AtomComponent atomComp = go.GetComponent<AtomComponent>();
        atomComp.UpdateModel( atomModel );
        _atoms.Add( atomModel.AtomicNumber, atomComp );
    }

    private void GenerateAtom( AbstractMessage message )
    {
        int randomAtomIndex = UnityEngine.Random.Range( 1, gameModel.User.Atoms.Count );
        if( AMM.GenerateAtom() )
        {
            
            Messenger.Dispatch( AtomMessage.ATOM_STOCK_UPDATED );
        }
    }

    private void AtomHarvested( AbstractMessage message )
    {
        AtomMessage data = message as AtomMessage;
        AMM.HarvestAtom( data.AtomicNumber );

        Messenger.Dispatch( AtomMessage.ATOM_STOCK_UPDATED );
    }

    private void AtomStockUpgrade( AbstractMessage msg )
    {
        AtomMessage message = msg as AtomMessage;
        if( AMM.UpgradeAtomStock( message.AtomicNumber ) )
        {
            Messenger.Dispatch( AtomMessage.ATOM_STOCK_UPDATED );
        }
    }

    private void DeductAtomsWorthSC( AbstractMessage msg )
    {
        AtomMessage message = msg as AtomMessage;
        AMM.SpendAtomsWorthSC( message.SC );
        Messenger.Dispatch( AtomMessage.ATOM_STOCK_UPDATED );
    }

    private void handleAtomStockUpdate( AbstractMessage message )
    {
        AtomMessage data = message as AtomMessage;
        AMM.UpdateAtomStock( data.AtomicNumber, data.Delta );
        Messenger.Dispatch( AtomMessage.ATOM_STOCK_UPDATED );
    }
    
}
