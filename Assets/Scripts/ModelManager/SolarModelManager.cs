using System.Collections.Generic;
using UnityEngine;

public class SolarModelManager
{
    private GameModel _gameModel;

    public void Setup( GameModel gameModel )
    {
        _gameModel = gameModel;
    }

    public SolarModel GenerateSolar( float SC )
    {
        Dictionary<int, int> stocks = new Dictionary<int, int>();
        Dictionary<int, float> atomWeights = new Dictionary<int, float>();
        
        int maxAtomicNumber = getMaxAtomicNumberFromSC( SC );
        for( int i = 1; i <= maxAtomicNumber; i++ )
        {
            stocks[ i ] = 0;

            atomWeights.Add
            (
                i,
                ( 1 / Mathf.Sqrt( 2 * Mathf.PI * _gameModel.Config.curve ) ) * Mathf.Exp( -Mathf.Pow( maxAtomicNumber - i, 2 ) / ( 2 * _gameModel.Config.curve ) )
            );
        }

        Dictionary<int, bool> chosenAtoms = new Dictionary<int, bool>();
        int minimum = maxAtomicNumber;
        if( minimum > 12 )
            minimum = 12;

        int currentAtomIndex;
        for( int i = 0; i < minimum; i++ )
        {
            currentAtomIndex = (int)chooseWithProbabilities( atomWeights );
            chosenAtoms[ currentAtomIndex ] = true;
        }
        
        float SCmin = SC * 1.5f;
        float SCPerAtom = SCmin / chosenAtoms.Count;
        float maxSCPerAtom = SC * _gameModel.Config.MaxHarvestMultiplier / chosenAtoms.Count;
        
        SolarModel solarModel = new SolarModel( new JSONSolarModel() );
        solarModel.Name = "Star " + _gameModel.User.StarsCreated;
        _gameModel.User.StarsCreated++;
        solarModel.Radius = (int)( SC );
        solarModel.Lifetime = (int)( SCPerAtom );
        solarModel.CreatedSC = SC;

        PlanetModel planetModel = new PlanetModel( new JSONPlanetModel() );
        planetModel.Name = "Planet " + _gameModel.User.PlanetsCreated;
        _gameModel.User.PlanetsCreated++;
        planetModel.Radius = 1;
        solarModel.Planets.Add( planetModel );

        AtomModel planetAtomModel;
        foreach( KeyValuePair<int, bool> item in chosenAtoms )
        {
            planetAtomModel = _gameModel.Atoms[ item.Key ].Copy();
            planetAtomModel.Stock = (int)( SCPerAtom / planetAtomModel.AtomicWeight);
            planetAtomModel.MaxStock = planetAtomModel.Stock;
            planetModel.Atoms.Add( planetAtomModel );
        }

        return solarModel;
    }

    public SolarModel oldGenerateSolar( float SC )
    {
        Dictionary<int, int> stocks = new Dictionary<int, int>();
        Dictionary<int, float> atomWeights = new Dictionary<int, float>();

        //string output = "weights: ";

        int maxAtomicNumber = getMaxAtomicNumberFromSC( SC );
        for( int i = 1; i <= maxAtomicNumber; i++ )
        {
            stocks[ i ] = 0;

            atomWeights.Add
            (
                i,
                ( 1 / Mathf.Sqrt( 2 * Mathf.PI * _gameModel.Config.curve ) ) * Mathf.Exp( -Mathf.Pow( maxAtomicNumber - i, 2 ) / ( 2 * _gameModel.Config.curve ) )
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
            currentAtomIndex = (int)chooseWithProbabilities( atomWeights );
            chosenAtoms[ currentAtomIndex ] = true;
        }

        float maxSCSolar = (int)( _gameModel.Config.MaxHarvestMultiplier * SC );
        float MaxSCSoFar = 0;
        bool MaxNeedMore = true;

        float SCSolar = ( SC / .1f );
        float SCSoFar = 0;
        bool NeedMore = true;

        int lifetime = 0;
        AtomModel currentAtom;
        while( MaxNeedMore )
        {
            foreach( KeyValuePair<int, bool> item in chosenAtoms )
            {
                currentAtom = _gameModel.Atoms[ item.Key ];
                if( SCSoFar + currentAtom.AtomicWeight > SCSolar )
                {
                    NeedMore = false;
                }
                else
                {
                    SCSoFar += currentAtom.AtomicWeight;
                }

                if( MaxSCSoFar + currentAtom.AtomicWeight > maxSCSolar )
                {
                    MaxNeedMore = false;
                    break;
                }
                else
                {
                    MaxSCSoFar += currentAtom.AtomicWeight;
                    stocks[ item.Key ] += 1;
                }
            }
            if( NeedMore )
                lifetime++;
        }

        SolarModel solarModel = new SolarModel( new JSONSolarModel() );
        solarModel.Name = "Star " + _gameModel.User.StarsCreated;
        _gameModel.User.StarsCreated++;
        solarModel.Radius = (int)( SC );
        solarModel.Lifetime = (int)( lifetime );
        solarModel.CreatedSC = SC;

        PlanetModel planetModel = new PlanetModel( new JSONPlanetModel() );
        planetModel.Name = "Planet " + _gameModel.User.PlanetsCreated;
        _gameModel.User.PlanetsCreated++;
        planetModel.Radius = 1;
        solarModel.Planets.Add( planetModel );

        AtomModel planetAtomModel;
        foreach( KeyValuePair<int, bool> item in chosenAtoms )
        {
            planetAtomModel = _gameModel.Atoms[ item.Key ].Copy();
            planetAtomModel.Stock = stocks[ item.Key ];
            planetModel.Atoms.Add( planetAtomModel );
        }

        return solarModel;
    }

    private int getMaxAtomicNumberFromSC( float SC )
    {
        int result = 1;
        foreach( KeyValuePair<int, int> range in _gameModel.Config.atomSCUnlockRanges )
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

    private float chooseWithProbabilities( Dictionary<int, float> probs )
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
