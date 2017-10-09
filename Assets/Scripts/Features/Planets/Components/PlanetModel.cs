using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UniRx;

public class PlanetModel
{
    public JSONPlanetModel Model;

    public StringReactiveProperty rName = new StringReactiveProperty();
    public IntReactiveProperty rRadius = new IntReactiveProperty(10);
    public IntReactiveProperty rDistance = new IntReactiveProperty(10);
    public ReactiveCollection<AtomModel> Atoms = new ReactiveCollection<AtomModel>();
    
    public PlanetModel( JSONPlanetModel model )
    {
        Model = model;

        rName.Value = Model.Name;
        rRadius.Value = Model.Radius;
        rDistance.Value = Model.Distance;

        for( int i = 0; i < model.Atoms.Count; i++ )
        {
            Atoms.Add( new AtomModel( Model.Atoms[ i ] ) );
        }
    }

    public string Name
    {
        set { Model.Name = value; rName.Value = value; }
        get { return rName.Value; }
    }

    public int Radius
    {
        set { Model.Radius = value; rRadius.Value = value; }
        get { return rRadius.Value; }
    }

    public int Distance
    {
        set { Model.Distance = value; rDistance.Value = value; }
        get { return rDistance.Value; }
    }
    
}
