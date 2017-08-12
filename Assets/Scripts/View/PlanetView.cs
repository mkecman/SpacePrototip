using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetView : MonoBehaviour {

    public GameModel gameModel;
    public Text UIName;
    public GameObject planetAtomPrefab;
    public GameObject _planetAtomsContainer;

    private PlanetModel _model;

    void Start()
    {
        
    }

    public void SetupView( PlanetModel model )
    {
        GameObject gm = GameObject.Find( "GameModel" );
        gameModel = gm.GetComponent<GameModel>();

        _model = model;
        Name = _model.Name;

        for( int index = 0; index < _model.AtomsAvailable.Length; index++ )
        {
            GameObject planetAtom = Instantiate( planetAtomPrefab, _planetAtomsContainer.transform );
            StoreView planetAtomStoreView = planetAtom.GetComponent<StoreView>();
            AtomModel atomDefinition = gameModel.getAtomByAtomicWeight( _model.AtomsAvailable[ index ] );
            planetAtomStoreView.Name = atomDefinition.Symbol;
            planetAtomStoreView.MaxStock = _model.AtomsStock[ index ];
            planetAtomStoreView.Stock = _model.AtomsStock[ index ];
            planetAtomStoreView.Property = _model.AtomsHarvestRates[ index ] + "";
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
