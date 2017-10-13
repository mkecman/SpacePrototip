using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using System;

public class SolarModel
{
    public StringReactiveProperty rName = new StringReactiveProperty("DefaultStar");
    public IntReactiveProperty rRadius = new IntReactiveProperty(10);
    public IntReactiveProperty rLifetime = new IntReactiveProperty(30);
    public FloatReactiveProperty rCreatedSC = new FloatReactiveProperty(10f);
    public ReactiveCollection<PlanetModel> Planets = new ReactiveCollection<PlanetModel>();

    public SolarModel( JSONSolarModel model )
    {
        rName.Value = model.Name;
        rRadius.Value = model.Radius;
        rCreatedSC.Value = model.CreatedSC;

        for( int i = 0; i < model.Planets.Count; i++ )
        {
            Planets.Add( new PlanetModel( model.Planets[ i ] ) );
        }
    }

    public JSONSolarModel toJSON()
    {
        JSONSolarModel model = new JSONSolarModel();
        model.Name = rName.Value;
        model.Radius = rRadius.Value;
        model.Lifetime = rLifetime.Value;
        model.CreatedSC = rCreatedSC.Value;

        model.Planets = new List<JSONPlanetModel>();
        for( int i = 0; i < Planets.Count; i++ )
        {
            model.Planets.Add( Planets[ i ].toJSON() );
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

    public int Lifetime
    {
        set { rLifetime.Value = value; }
        get { return rLifetime.Value; }
    }

    public float CreatedSC
    {
        set { rCreatedSC.Value = value; }
        get { return rCreatedSC.Value; }
    }
    
}
