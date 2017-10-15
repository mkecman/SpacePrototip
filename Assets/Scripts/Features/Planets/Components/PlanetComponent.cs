using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PlanetComponent : AbstractView
{
    public Text UIName;
    public GameObject planetAtomPrefab;
    public GameObject planetAtomsContainer;
    
    private PlanetModel _model;
    //private List<PlanetAtomComponent> atoms;
    
    void OnDestroy()
    {
        _model = null;
    }

    public void Setup( PlanetModel model )
    {
        _model = model;
        UIName.text = _model.Name;

        //atoms = new List<PlanetAtomComponent>();
        for( int i = 0; i < _model.Atoms.Count; i++ )
        {
            GameObject planetAtomPrefabInstance = Instantiate( planetAtomPrefab, planetAtomsContainer.transform );
            PlanetAtomComponent planetAtomComp = planetAtomPrefabInstance.GetComponent<PlanetAtomComponent>();
            planetAtomComp.UpdateModel( _model.Atoms[ i ] );
            //atoms.Add( planetAtomComp );
        }
    }
    
}
