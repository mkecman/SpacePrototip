using UnityEngine;
using System.Collections;

public class PlanetAtomComponent : AbstractView
{
    private PlanetAtomModel _model;

    public void Setup( PlanetAtomModel model )
    {
        _model = model;

        AtomModel atomDefinition = gameModel.Atoms[ model.AtomicNumber ];
        StoreComponent planetAtomStore = gameObject.GetComponent<StoreComponent>();
        planetAtomStore.ID = atomDefinition.AtomicNumber + "";
        planetAtomStore.Name = atomDefinition.Symbol;
        planetAtomStore.MaxStock = model.Stock;
        planetAtomStore.Stock = model.Stock;

        PlanetAtomUpgradeComponent upgradeComp = gameObject.GetComponent<PlanetAtomUpgradeComponent>();
        upgradeComp.Setup( model );

        HarvesterComponent harvester = gameObject.GetComponent<HarvesterComponent>();
        harvester.Setup( planetAtomStore, model );

    }


}
