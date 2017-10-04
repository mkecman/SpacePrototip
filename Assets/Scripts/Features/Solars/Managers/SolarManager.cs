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

    Dictionary<int, int> atomSCUnlockRanges;

    Vector3 p0;
    Vector3 p1;
    Vector3 p2;
    Vector3 p3;

    void Start()
    {
        Messenger.Listen( SolarMessage.CREATE_SOLAR, handleCreateSolar );
        Messenger.Listen(GameMessage.MODEL_LOADED, handleGameModelLoaded);
        p0 = new Vector3( 0.0f, 0.0f );
        p1 = new Vector3( 15000.0f, 30.0f );
        p2 = new Vector3( 120000.0f, 60.0f );
        p3 = new Vector3( 150000.0f, 118.0f );

        atomSCUnlockRanges = new Dictionary<int, int>();
        atomSCUnlockRanges[ 1 ] = 0;
        atomSCUnlockRanges[ 2 ] = 55;
        atomSCUnlockRanges[ 3 ] = 320;
        atomSCUnlockRanges[ 4 ] = 699;
        atomSCUnlockRanges[ 5 ] = 1557;
        atomSCUnlockRanges[ 6 ] = 2357;
        atomSCUnlockRanges[ 7 ] = 3271;
        atomSCUnlockRanges[ 8 ] = 4307;
        atomSCUnlockRanges[ 9 ] = 5468;
        atomSCUnlockRanges[ 10 ] = 6777;
        atomSCUnlockRanges[ 11 ] = 8215;
        atomSCUnlockRanges[ 12 ] = 9796;
        atomSCUnlockRanges[ 13 ] = 11508;
        atomSCUnlockRanges[ 14 ] = 13361;
        atomSCUnlockRanges[ 15 ] = 15342;
        atomSCUnlockRanges[ 16 ] = 17461;
        atomSCUnlockRanges[ 17 ] = 19709;
        atomSCUnlockRanges[ 18 ] = 22101;
        atomSCUnlockRanges[ 19 ] = 24662;
        atomSCUnlockRanges[ 20 ] = 27357;
    }

    int getMaxAtomicNumberFromSC( float SC )
    {
        int result = 1;
        foreach( KeyValuePair<int,int> range in atomSCUnlockRanges )
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

    void Update()
    {
        if( _layoutUpdateCount < 30 )
        {
            LayoutRebuilder.MarkLayoutForRebuild( galaxy.transform as RectTransform );
            _layoutUpdateCount++;
        }
    }
    
    Vector3 getBezier( float time )
    {
        return Bezier.GetPoint( p0, p1, p2, p3, time );
    }

    float yFromX( float xTarget )
    {
        
        float xTolerance = 0.1f; //adjust as you please
        
        //establish bounds
        float lower = 0.0f;
        float upper = 1.0f;
        float percent = ( upper + lower ) / 2;

        //get initial x
        float x = getBezier( percent ).x;

        //loop until completion
        while(Mathf.Abs(xTarget - x) > xTolerance)
        {
            if(xTarget > x) 
                lower = percent;
            else 
                upper = percent;

            percent = (upper + lower) / 2;
            x = getBezier( percent ).x;
        }
        //we're within tolerance of the desired x value.
        //return the y value.
        return getBezier( percent ).y;
    }

    private void getRanges()
    {
        Dictionary<int, Vector2> ranges = new Dictionary<int, Vector2>();
        Vector3 bezier;
        int currentY = 0;
        float maxX = 0;

        for( float i = 0.00001f; i <= 1.0f; i += 0.00001f )
        {
            bezier = getBezier( i );

            if( bezier.x > maxX )
            {
                maxX = bezier.x;
            }

            if( (int)Mathf.Ceil( bezier.y ) > currentY )
            {
                currentY = (int)Mathf.Ceil( bezier.y );
                ranges[ currentY ] = new Vector2( bezier.x, 0.0f );
            }
            else
            {
                ranges[ currentY ] = new Vector2( ranges[ currentY ].x, maxX );
            }
        }

        Debug.Log( ranges );
    }


    private void handleCreateSolar( AbstractMessage message )
    {
        float SC = ( message as SolarMessage ).SC;
        
        /**/
        if( SC >= gameModel.User.SC )
        {
            Debug.Log( "Not enough SC to create Solar system!" );
            return;
        }

        Messenger.Dispatch( AtomMessage.DEDUCT_ATOMS_WORTH_SC, new AtomMessage( 0, 0, SC ) );
        /**/

        float minSC = gameModel.Config.minSC;
        float maxSC = gameModel.Config.maxSC;
        float minLT = 90;
        float maxLT = 3220;
        float minEL = 1;
        float maxEL = gameModel.Config.MaxAtomicNumber;
        
        float minST = SC * 2.0f;
        float maxST = SC * 100.0f;

        SolarModel solarModel = new SolarModel();
        solarModel.Name = "Star " + _starsCreated;
        _starsCreated++;
        solarModel.Radius = (int)( SC );
        float factorLT = ( maxLT - minLT ) / ( maxSC - minSC );
        //solarModel.Lifetime = (int)( ( factorLT * ( SC - minSC ) ) + minLT );
        solarModel.Lifetime = (int)SC * 5;


        ///
        //Randomizing Atoms amount for the whole Solar
        ///
        int maxAtomicNumber = getMaxAtomicNumberFromSC( SC );//(int)Math.Ceiling( yFromX( SC ) );

        int given = (int)maxST;
        float total = 0;
        int index = 1;
        bool needMore = true;
        Dictionary<int, int> pieces = new Dictionary<int, int>();
        Dictionary<int, float> atomWeights = new Dictionary<int, float>();
        float curve = 10.0f;
        for( int i = 1; i <= maxAtomicNumber; i++ )
        {
            pieces[ i ] = 0;

            atomWeights.Add
            (
                i,
                ( 1 / Mathf.Sqrt( 2 * Mathf.PI * i ) ) * Mathf.Exp( -Mathf.Pow( maxAtomicNumber - i, 2 ) / ( 2 * i ) )
              //( 1 / Mathf.Sqrt( 2 * Mathf.PI * curve ) ) * Mathf.Exp( -Mathf.Pow( maxAtomicNumber - i, 2 ) / ( 2 * curve ) )
            );
        }

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


        

        PlanetModel planetModel = new PlanetModel();
        PlanetAtomModel planetAtomModel;
        planetModel = new PlanetModel();
        planetModel.Name = "Planet " + _planetsCreated;
        planetModel.Radius = _planetsCreated;
        _planetsCreated++;
        solarModel.Planets.Add( planetModel );

        foreach( KeyValuePair<int,bool> item in chosenAtoms )
        {
            planetAtomModel = new PlanetAtomModel();
            planetAtomModel.AtomicNumber = item.Key;
            planetAtomModel.Stock = 9999;
            planetAtomModel.HarvestRate = 1;
            planetAtomModel.UpgradeLevel = 0;
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
