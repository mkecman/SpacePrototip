using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class SolarManager : AbstractController
{
    public GameObject galaxy;
    public GameObject solarPrefab;

    private int _starsCreated = 1;
    private int _planetsCreated = 1;
    private int _layoutUpdateCount = 0;
    
    private List<SolarComponent> solars = new List<SolarComponent>();

    void Start()
    {
        Messenger.Listen( SolarMessage.CREATE_SOLAR, handleCreateSolar );
        Messenger.Listen(GameMessage.MODEL_LOADED, handleGameModelLoaded);
    }

    void Update()
    {
        if( _layoutUpdateCount < 30 )
        {
            LayoutRebuilder.MarkLayoutForRebuild( galaxy.transform as RectTransform );
            _layoutUpdateCount++;
        }
    }

    private void handleCreateSolar( AbstractMessage message )
    {
        float SC = ( message as SolarMessage ).SC;

        if( SC >= gameModel.User.SC )
        {
            Debug.Log( "Not enough SC to create Solar system!" );
            return;
        }

        Messenger.Dispatch( AtomMessage.DEDUCT_ATOMS_WORTH_SC, new AtomMessage( 0, 0, SC ) );
        
        float minSC = gameModel.minSC;
        float maxSC = gameModel.maxSC;
        float minLT = 60;
        float maxLT = 1440;
        float minEL = 1;
        float maxEL = gameModel.atomsCount;
        
        float minST = SC * 2.0f;
        float maxST = SC * 10.0f;

        SolarModel solarModel = new SolarModel();
        solarModel.Name = "Star " + _starsCreated;
        _starsCreated++;
        solarModel.Radius = (int)( SC );
        float factorLT = ( maxLT - minLT ) / ( maxSC - minSC );
        solarModel.Lifetime = (int)( ( factorLT * ( SC - minSC ) ) + minLT );
        //Debug.Log( "SOLAR: "+ solarModel.Name + " created. -------------" );


        ///
        //Randomizing Atoms amount for the whole Solar
        ///
        float factorEL = ( maxEL - minEL ) / ( maxSC - minSC );
        int maxAtomicNumber = (int)Math.Floor( ( factorEL * ( SC - minSC ) ) + minEL );
        
        int given = (int)maxST;
        float total = 0;
        int index = 1;
        bool needMore = true;
        Dictionary<int, int> pieces = new Dictionary<int, int>();
        Dictionary<int, float> atomWeights = new Dictionary<int, float>();
        float curve = 2.0f;
        for( int i = 1; i <= maxAtomicNumber; i++ )
        {
            pieces[ i ] = 0;

            atomWeights.Add
            (
                i,
                ( 1 / Mathf.Sqrt( 2 * Mathf.PI * curve ) ) * Mathf.Exp( -Mathf.Pow( maxAtomicNumber - i, 2 ) / ( 2 * curve ) )
            );
        }

        while( needMore )
        {
            float currentAtomWeight = gameModel.Atoms[ index ].AtomicWeight;
            if( total + currentAtomWeight <= given )
            {
                total += currentAtomWeight;
                pieces[ index ] += 1;
                index = (int)Choose( atomWeights );
            }
            else
            {
                needMore = false;
            }
        }
        ///
        //END Randomizing Atoms amount for the whole Solar
        ///

        PlanetModel planetModel = new PlanetModel();
        PlanetAtomModel planetAtomModel;
        int addedAtoms = 0;
        int maxAtomsPerPlanet = 3;
        for( int i = 1; i <= pieces.Count; i++ )
        {
            if( addedAtoms % maxAtomsPerPlanet == 0 )
            {
                planetModel = new PlanetModel();
                planetModel.Name = "Planet " + _planetsCreated;
                planetModel.Radius = _planetsCreated;
                _planetsCreated++;
                solarModel.Planets.Add( planetModel );
            }

            if( pieces[ i ] > 0 )
            {
                planetAtomModel = new PlanetAtomModel();
                planetAtomModel.AtomicNumber = i;
                planetAtomModel.Stock = pieces[ i ];
                planetAtomModel.HarvestRate = 1;
                planetAtomModel.UpgradeLevel = 0;
                planetModel.Atoms.Add( planetAtomModel );
                addedAtoms++;
            }
        }

        GameObject solarPrefabInstance = Instantiate( solarPrefab, galaxy.transform );
        SolarComponent solar = solarPrefabInstance.GetComponent<SolarComponent>();

        solars.Add( solar );
        gameModel.User.Galaxies.Add( solarModel );

        solar.Setup( solarModel );
        Messenger.Dispatch( SolarMessage.SOLAR_CREATED );

        _layoutUpdateCount = 0; //fix for layoutgroup update, check Update() function;

        //Debug.Log( "-------------" );

    }
 
    void handleGameModelLoaded( AbstractMessage message )
    {
        int galaxiesCount = gameModel.User.Galaxies.Count;
        if ( galaxiesCount > 0 )
        {
            for (int solarIndex = 0; solarIndex < galaxiesCount; solarIndex++)
            {
                GameObject solarPrefabInstance = Instantiate(solarPrefab, galaxy.transform);
                SolarComponent solar = solarPrefabInstance.GetComponent<SolarComponent>();
                solar.Setup(gameModel.User.Galaxies[solarIndex]);
            }
        }
    }

    float Choose( Dictionary<int, float> probs )
    {

        float total = 0;

        foreach( KeyValuePair<int, float> elem in probs )
        {
            total += elem.Value;
        }

        float randomPoint = UnityEngine.Random.value * total;

        for( int i = 1; i <= probs.Count; i++ )
        {
            if( randomPoint < probs[ i ] )
            {
                return i;
            }
            else
            {
                randomPoint -= probs[ i ];
            }
        }
        return probs.Count;
    }
    
    
}
