﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetComponent : AbstractView
{
    public Text UIName;
    public GameObject planetAtomPrefab;
    public GameObject planetAtomsContainer;
    
    private PlanetModel _model;
    //private List<PlanetAtomComponent> atoms = new List<PlanetAtomComponent>();
    
    void OnDestroy()
    {
        _model = null;
    }

    public void Setup( PlanetModel model )
    {
        _model = model;
        UIName.text = _model.Name;

        for( int i = 0; i < _model.Atoms.Count; i++ )
        {
            GameObject planetAtomPrefabInstance = Instantiate( planetAtomPrefab, planetAtomsContainer.transform );
            PlanetAtomComponent planetAtomComp = planetAtomPrefabInstance.GetComponent<PlanetAtomComponent>();
            planetAtomComp.Setup( _model.Atoms[ i ] );
            //atoms.Add( planetAtomComp );
        }
    }
}
