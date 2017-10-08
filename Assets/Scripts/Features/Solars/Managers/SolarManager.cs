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
        Messenger.Listen( SolarMessage.SOLAR_DESTROYED, handleSolarDestroyed );
    }

    private void handleSolarDestroyed( AbstractMessage message )
    {
        gameModel.User.Galaxies.Remove( ( message as SolarMessage ).model );
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
        float maxSC = (message as SolarMessage).maxSC;
        
        /**/
        if( SC > gameModel.User.SC )
        {
            Debug.Log( "Not enough SC to create Solar system!" );
            return;
        }

        Messenger.Dispatch( AtomMessage.DEDUCT_ATOMS_WORTH_SC, new AtomMessage( 0, 0, SC ) );
        /**/
        
        SolarModel solarModel = new SolarModel();
        _starsCreated++;
        solarModel.Name = "Star " + _starsCreated;
        solarModel.Radius = (int)( SC );
        
        int maxAtomicNumber = getMaxAtomicNumberFromSC( SC );
        
        Dictionary<int, int> stocks = new Dictionary<int, int>();
        Dictionary<int, float> atomWeights = new Dictionary<int, float>();
        float curve = gameModel.Config.curve;

        //string output = "weights: ";

        for( int i = 1; i <= maxAtomicNumber; i++ )
        {
            stocks[ i ] = 0;

            atomWeights.Add
            (
                i,
                ( 1 / Mathf.Sqrt( 2 * Mathf.PI * curve ) ) * Mathf.Exp( -Mathf.Pow(maxAtomicNumber - i, 2 ) / ( 2 * curve ) )
            );
            //output += i + ":"+Math.Round( 100f * atomWeights[i] ) + ", ";
        }
        //Debug.Log(output);

        Dictionary<int, bool> chosenAtoms = new Dictionary<int, bool>();
        int minimum = maxAtomicNumber;
        if( minimum > 12 )
            minimum = 12;

        int currentAtomIndex;
        for( int i = 0; i < minimum; i++ )
        {
            currentAtomIndex = (int)Choose( atomWeights );
            chosenAtoms[ currentAtomIndex ] = true;
        }
        
        float maxSCSolar = (int)( gameModel.Config.MaxHarvestRate * SC );
        float MaxSCSoFar = 0;
        bool MaxNeedMore = true;

        float SCSolar = ( SC / .1f );
        float SCSoFar = 0;
        bool NeedMore = true;

        int lifetime = 0;
        while (MaxNeedMore)
        {
            foreach (KeyValuePair<int, bool> item in chosenAtoms)
            {
                if( SCSoFar + gameModel.Atoms[ item.Key ].AtomicWeight > SCSolar )
                {
                    NeedMore = false;
                }
                else
                {
                    SCSoFar += gameModel.Atoms[ item.Key ].AtomicWeight;
                }

                if( MaxSCSoFar + gameModel.Atoms[ item.Key ].AtomicWeight > maxSCSolar )
                {
                    MaxNeedMore = false;
                    break;
                }
                else
                {
                    MaxSCSoFar += gameModel.Atoms[item.Key].AtomicWeight;
                    stocks[item.Key] += 1;
                }
            }
            if( NeedMore )
                lifetime++;
        }

        solarModel.Lifetime = (int)(lifetime);

        AtomModel planetAtomModel;
        PlanetModel planetModel = new PlanetModel();
        planetModel.Name = "Planet " + _planetsCreated;
        planetModel.Radius = _planetsCreated;
        _planetsCreated++;
        solarModel.Planets.Add( planetModel );

        foreach( KeyValuePair<int,bool> item in chosenAtoms )
        {
            planetAtomModel = new AtomModel();
            planetAtomModel.AtomicNumber = item.Key;
            planetAtomModel.Stock = stocks[ item.Key ];
            planetModel.Atoms.Add( planetAtomModel );
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
        _layoutUpdateCount = 0;
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
    
    int getMaxAtomicNumberFromSC( float SC )
    {
        int result = 1;
        foreach( KeyValuePair<int, int> range in gameModel.Config.atomSCUnlockRanges )
        {
            if( SC >= range.Value )
            {
                result = range.Key;
            }
            else
                break;
        }

        return result;
    }
}
