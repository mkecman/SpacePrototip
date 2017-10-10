using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UniRx;

public class SolarModel
{
    public JSONSolarModel Model;

    public StringReactiveProperty rName = new StringReactiveProperty("DefaultStar");
    public IntReactiveProperty rRadius = new IntReactiveProperty(10);
    public IntReactiveProperty rLifetime = new IntReactiveProperty(30);
    public FloatReactiveProperty rCreatedSC = new FloatReactiveProperty(10f);
    public ReactiveCollection<PlanetModel> Planets = new ReactiveCollection<PlanetModel>();

    public SolarModel( JSONSolarModel model )
    {
        Model = model;

        rName.Value = Model.Name;
        rRadius.Value = Model.Radius;
        rCreatedSC.Value = Model.CreatedSC;

        for( int i = 0; i < model.Planets.Count; i++ )
        {
            Planets.Add( new PlanetModel( Model.Planets[ i ] ) );
        }
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
