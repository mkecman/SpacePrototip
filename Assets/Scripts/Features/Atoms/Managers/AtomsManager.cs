using System.Collections.Generic;
using UnityEngine;
using UniRx;

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
        Messenger.Listen( AtomMessage.SPEND_ATOMS, SpendAtoms );
    }

    private void SetupAtoms( AbstractMessage message )
    {
        gameModel.User.Atoms.ObserveAdd().Subscribe( addEvent => CreateAtom( addEvent.Value ) ).AddTo(this);

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
        AMM.GenerateAtom();
    }

    private void AtomHarvested( AbstractMessage message )
    {
        AtomMessage data = message as AtomMessage;
        AMM.HarvestAtom( data.AtomicNumber );
    }

    private void AtomStockUpgrade( AbstractMessage msg )
    {
        AtomMessage message = msg as AtomMessage;
        AMM.UpgradeAtomStock( message.AtomicNumber );
    }

    private void SpendAtoms( AbstractMessage msg )
    {
        AtomMessage message = msg as AtomMessage;
        AMM.SpendAtoms( message.SC );
    }

    

}
