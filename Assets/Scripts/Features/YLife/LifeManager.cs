using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.UI;

public class LifeManager : AbstractController
{
    public Transform planetContainer;
    public Transform lifeContainer;

    private LifeModel life;
    private YPlanetModel planet;
    private FloatReactiveProperty _planetAttack = new FloatReactiveProperty();
    private FloatReactiveProperty _lifeResistance = new FloatReactiveProperty();


    private void OnEnable()
    {
        Observable.Interval( TimeSpan.FromSeconds( 1 ) ).Subscribe( UpdateLife );
    }

    private void Awake()
    {
        TextAsset targetFile = Resources.Load<TextAsset>( "Configs/LifeConfig" );
        life = new LifeModel( JsonUtility.FromJson<JSONLifeModel>( targetFile.text ) );

        Messenger.Listen( GameMessage.MODEL_LOADED, Setup );
    }

    private void UpdateLife( long time )
    {
        life.PopulationGrowth = ( life.Farmers * ( 1.5f - _planetAttack.Value ) - Mathf.FloorToInt(life.Population) ) / 10;
        if( life.PopulationGrowth >= 0 )
            life.Population += life.PopulationGrowth;

        life.ScienceGrowth = ( life.Scientists * ( 1.5f - _planetAttack.Value ) - Mathf.FloorToInt( life.Population ) ) / 10;
        if( life.ScienceGrowth >= 0 )
            life.Science += life.ScienceGrowth;
        
        //Debug.Log( "--------------" );
    }
    
    private void Setup( AbstractMessage message )
    {
        setupPlanet();
        setupLife();
    }

    private void setupLife()
    {
        setupLifeFloatProperty( "PlanetAttack", _planetAttack );
        setupLifeFloatProperty( "LifeResistance", _lifeResistance );

        setupLifeFloatProperty( "Population", life.rPopulation );
        setupLifeFloatProperty( "PopulationGrowth", life.rPopulationGrowth );
        setupLifeFloatProperty( "Science", life.rScience );
        setupLifeFloatProperty( "ScienceGrowth", life.rScienceGrowth );
        setupLifePopulationProperty( "Farmers", life.rFarmers );
        setupLifePopulationProperty( "Scientists", life.rScientists );
        setupLifePopulationProperty( "Miners", life.rMiners );

        setupResistance( PlanetaryEnvironment.Temperature );
        setupResistance( PlanetaryEnvironment.Pressure );
        setupResistance( PlanetaryEnvironment.Gravity );
        setupResistance( PlanetaryEnvironment.Magnetic );
        setupResistance( PlanetaryEnvironment.Light );
    }

    private void setupResistance( string environment )
    {
        Transform child = lifeContainer.Find( environment + "Resistance" );
        LifeResistance resistance = child.GetComponent<LifeResistance>();
        resistance.Setup( environment, planet, life );
    }

    private void setupLifePopulationProperty( string name, FloatReactiveProperty property )
    {
        Transform child = lifeContainer.Find( name + "PopulationProperty" );
        PopulationProperty lfp = child.GetComponent<PopulationProperty>();
        lfp.Setup( property );
    }

    private void setupLifeFloatProperty( string name, FloatReactiveProperty property )
    {
        Transform child = lifeContainer.Find( name + "LifeProperty" );
        LifeFloatProperty lfp = child.GetComponent<LifeFloatProperty>();
        lfp.Setup( name, property );
    }

    private void setupPlanet()
    {
        planet = new YPlanetModel();
        planet.Name = "Test";
        setupEnvironment( PlanetaryEnvironment.Temperature );
        setupEnvironment( PlanetaryEnvironment.Pressure );
        setupEnvironment( PlanetaryEnvironment.Gravity );
        setupEnvironment( PlanetaryEnvironment.Magnetic );
        setupEnvironment( PlanetaryEnvironment.Light );
    }

    private void setupEnvironment( string environment )
    {
        Transform child = planetContainer.Find( "PP-" + environment );
        PlanetProperty pp = child.GetComponent<PlanetProperty>();
        
        pp.Setup( gameModel.PlanetaryEnvironments[ environment ] );
        pp.Model.rStrength.Subscribe( _ => updatePlanetAttack() );
        pp.Model.rCurrentValue.Subscribe( _ => updateLifePlanetaryResistance( environment, _ ) );
        planet.environments.Add( environment, pp );
    }

    private void updateLifePlanetaryResistance( string environment, float value )
    {
        Transform child = lifeContainer.Find( environment + "Resistance" );
        LifeResistance resistance = child.GetComponent<LifeResistance>();
        resistance.UpdateChartPlanetPoint( value );
    }

    private void updatePlanetAttack()
    {
        float totalAttack = 0;
        float totalResistance = 0;
        foreach( KeyValuePair<string, PlanetProperty> item in planet.environments )
        {
            float attack = getStrength( item.Key );
            float factor = 1 / ( item.Value.Model.MaxValue - item.Value.Model.MinValue );

            float resistance = life.resistances[ item.Key ].Max * factor;
            if( attack < 0 )
                resistance = life.resistances[ item.Key ].Min * factor;

            totalResistance += resistance;
            totalAttack += Mathf.Abs( attack );// - resistance;
        }
        _planetAttack.Value = totalAttack;
        _lifeResistance.Value = totalResistance;
    }

    private float getStrength( string environment )
    {
        return planet.environments[ environment ].Model.Strength;
    }
    
}