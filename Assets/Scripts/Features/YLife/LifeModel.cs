using UnityEngine;
using UniRx;
using System;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

public class LifeModel
{
    public StringReactiveProperty rName = new StringReactiveProperty();
    
    public FloatReactiveProperty rPopulation = new FloatReactiveProperty();
    public FloatReactiveProperty rPopulationGrowth = new FloatReactiveProperty();
    public FloatReactiveProperty rScience = new FloatReactiveProperty();
    public FloatReactiveProperty rScienceGrowth = new FloatReactiveProperty();
    public FloatReactiveProperty rFarmers = new FloatReactiveProperty();
    public FloatReactiveProperty rScientists = new FloatReactiveProperty();
    public FloatReactiveProperty rMiners = new FloatReactiveProperty();

    public FloatReactiveProperty rResistance = new FloatReactiveProperty();
    public Dictionary<string, JSONLifeResistanceModel> resistances = new Dictionary<string, JSONLifeResistanceModel>();

    public Dictionary<string, JSONLifeHarvestModel> harvesters = new Dictionary<string, JSONLifeHarvestModel>();

    public LifeModel( JSONLifeModel model )
    {
        fromJSON( model );
    }

    public LifeModel Copy()
    {
        return new LifeModel( this.toJSON() );
    }

    public void fromJSON( JSONLifeModel model )
    {
        rName.Value = model.Name;
        rPopulation.Value = model.Population;
        rPopulationGrowth.Value = model.PopulationGrowth;
        rScience.Value = model.Science;
        rScienceGrowth.Value = model.ScienceGrowth;
        rFarmers.Value = model.Farmers;
        rScientists.Value = model.Scientists;
        rMiners.Value = model.Miners;

        int i;
        for( i = 0; i < model.Resistance.Count; i++ )
        {
            resistances.Add( model.Resistance[ i ].Symbol, model.Resistance[ i ] );
        }

        for( i = 0; i < model.Harvesters.Count; i++ )
        {
            harvesters.Add( model.Harvesters[ i ].Symbol, model.Harvesters[ i ] );
        }
    }

    public JSONLifeModel toJSON()
    {
        JSONLifeModel model = new JSONLifeModel();

        model.Name = rName.Value;
        model.Population = rPopulation.Value;
        model.PopulationGrowth = rPopulationGrowth.Value;
        model.Science = rScience.Value;
        model.ScienceGrowth = rScienceGrowth.Value;
        model.Farmers = rFarmers.Value;
        model.Scientists = rScientists.Value;
        model.Miners = rMiners.Value;

        model.Resistance = new List<JSONLifeResistanceModel>();
        foreach( KeyValuePair<string, JSONLifeResistanceModel> item in resistances )
        {
            model.Resistance.Add( item.Value );
        }
        
        model.Harvesters = new List<JSONLifeHarvestModel>();
        foreach( KeyValuePair<string, JSONLifeHarvestModel> item in harvesters )
        {
            model.Harvesters.Add( item.Value );
        }
        
        return model;
    }

    public string Name
    {
        set { rName.Value = value; }
        get { return rName.Value; }
    }
    public float Population
    {
        set { rPopulation.Value = value; }
        get { return rPopulation.Value; }
    }
    public float PopulationGrowth
    {
        set { rPopulationGrowth.Value = value; }
        get { return rPopulationGrowth.Value; }
    }
    public float Science
    {
        set { rScience.Value = value; }
        get { return rScience.Value; }
    }
    public float ScienceGrowth
    {
        set { rScienceGrowth.Value = value; }
        get { return rScienceGrowth.Value; }
    }
    public float Farmers
    {
        set { rFarmers.Value = value; }
        get { return rFarmers.Value; }
    }
    public float Scientists
    {
        set { rScientists.Value = value; }
        get { return rScientists.Value; }
    }
    public float Miners
    {
        set { rMiners.Value = value; }
        get { return rMiners.Value; }
    }

}
