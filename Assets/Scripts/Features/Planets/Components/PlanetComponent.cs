using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetComponent : AbstractView
{
    public Text UIName;
    public GameObject planetPrefab;
    public GameObject planetAtomPrefab;
    public GameObject planetAtomsContainer;

    private PlanetModel _model;

    void Start()
    {
        
    }

    public void SetupView( PlanetModel model )
    {
        _model = model;
        Name = _model.Name;

        for( int index = 0; index < _model.AtomsAvailable.Length; index++ )
        {
            GameObject planetAtomPrefabInstance = Instantiate( planetAtomPrefab, planetAtomsContainer.transform );
            StoreComponent planetAtomStore = planetAtomPrefabInstance.GetComponent<StoreComponent>();
            AtomModel atomDefinition = gameModel.getAtomByAtomicWeight( _model.AtomsAvailable[ index ] );
            planetAtomStore.Name = atomDefinition.Symbol;
            planetAtomStore.MaxStock = _model.AtomsStock[ index ];
            planetAtomStore.Stock = _model.AtomsStock[ index ];
            planetAtomStore.Property = _model.AtomsHarvestRates[ index ] + "";
        }
    }
    
    public string Name
    {
        set
        {
            UIName.text = value;
        }
    }
}
