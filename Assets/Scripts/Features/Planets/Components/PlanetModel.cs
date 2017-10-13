using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using System;

public class PlanetModel
{
    public StringReactiveProperty rName = new StringReactiveProperty();
    public IntReactiveProperty rRadius = new IntReactiveProperty(10);
    public IntReactiveProperty rDistance = new IntReactiveProperty(10);
    public ReactiveCollection<AtomModel> Atoms = new ReactiveCollection<AtomModel>();
    
    public PlanetModel( JSONPlanetModel model )
    {
        rName.Value = model.Name;
        rRadius.Value = model.Radius;
        rDistance.Value = model.Distance;

        for( int i = 0; i < model.Atoms.Count; i++ )
        {
            Atoms.Add( new AtomModel( model.Atoms[ i ] ) );
        }
    }

    public JSONPlanetModel toJSON()
    {
        JSONPlanetModel model = new JSONPlanetModel();

        model.Name = rName.Value;
        model.Radius = rRadius.Value;
        model.Distance = rDistance.Value;

        model.Atoms = new List<JSONAtomModel>();
        for( int i = 0; i < Atoms.Count; i++ )
        {
            model.Atoms.Add( Atoms[ i ].toJSON() );
        }

        return model;
    }

    public string Name
    {
        set { rName.Value = value; }
        get { return rName.Value; }
    }

    public int Radius
    {
        set { rRadius.Value = value; }
        get { return rRadius.Value; }
    }

    public int Distance
    {
        set { rDistance.Value = value; }
        get { return rDistance.Value; }
    }

    
}
