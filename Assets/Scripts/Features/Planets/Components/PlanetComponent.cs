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
    public bool _isHarvesting = true;

    private PlanetModel _model;
    private List<StoreComponent> atoms = new List<StoreComponent>();
    private float _lastTime;

    void Update()
    {
        if( _isHarvesting && Time.time - _lastTime > 1.0f )
        {
            LayoutElement rt = gameObject.GetComponent<LayoutElement>();
            RectTransform rt2 = (RectTransform)planetAtomsContainer.transform;
            rt.minHeight = 30 + rt2.rect.height;

            _lastTime = Time.time;

            int remainingStock = 0;
            StoreComponent atomStore;

            for( int index = 0; index < atoms.Count; index++ )
            {
                atomStore = atoms[ index ];
                int atomicNumber = int.Parse( atomStore.ID );
                if( atomStore.Stock > 0 )
                {
                    remainingStock = atomStore.Stock - _model.Atoms[ atomicNumber ].HarvestRate;
                    if( remainingStock >= 0 )
                    {
                        atomStore.Stock = remainingStock;
                        _model.Atoms[ atomicNumber ].Stock = remainingStock;
                        Messenger.Dispatch( AtomMessage.ATOM_STOCK_CHANGED, new AtomMessage( atomicNumber, _model.Atoms[ atomicNumber ].HarvestRate ) );
                    }
                    else
                    {
                        Messenger.Dispatch( AtomMessage.ATOM_STOCK_CHANGED, new AtomMessage( atomicNumber, 0 ) );
                        atomStore.Stock = 0;
                        _model.Atoms[ atomicNumber ].Stock = 0;
                    }
                }
            }
        }
    }

    void OnDestroy()
    {
        //Debug.Log("planet destroyed");   
    }

    public void Setup( PlanetModel model )
    {
        _model = model;
        Name = _model.Name;

        foreach( KeyValuePair<int, PlanetAtomModel> kvp in _model.Atoms )
        {
            GameObject planetAtomPrefabInstance = Instantiate( planetAtomPrefab, planetAtomsContainer.transform );
            StoreComponent planetAtomStore = planetAtomPrefabInstance.GetComponent<StoreComponent>();
            AtomModel atomDefinition = gameModel.getAtomByAtomicNumber( kvp.Key );
            planetAtomStore.Name = atomDefinition.Symbol;
            planetAtomStore.ID = atomDefinition.AtomicNumber + "";
            planetAtomStore.MaxStock = kvp.Value.Stock;
            planetAtomStore.Stock = kvp.Value.Stock;

            HarvesterComponent harvester = planetAtomPrefabInstance.GetComponent<HarvesterComponent>();
            harvester.Setup( kvp.Value );
            
            atoms.Add( planetAtomStore );
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
