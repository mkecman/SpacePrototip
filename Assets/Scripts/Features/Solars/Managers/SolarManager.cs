using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public class SolarManager : AbstractController
{
    public GameObject galaxy;
    public GameObject solarPrefab;
    
    private List<SolarComponent> solars = new List<SolarComponent>();

    void Start()
    {
        Messenger.Listen( SolarMessage.CREATE_SOLAR, handleCreateSolar );
    }

    private void handleCreateSolar( AbstractMessage message )
    {
        float SC = ( message as SolarMessage ).SC;

        float minSC = 5;
        float maxSC = 1000;
        float minLT = 30;
        float maxLT = 120;
        float minPL = 1;
        float maxPL = 8;
        float minAT = 1;
        float maxAT = 4;
        float minEL = 1;
        float maxEL = gameModel.User.AtomsUnlocked + 1;
        float minST = 50;
        float maxST = 100;

        float factorLT = ( maxLT - minLT ) / ( maxSC - minSC );

        float factorPL = ( maxPL - minPL ) / ( maxSC - minSC );
        int maxPLValue = (int)Math.Round( ( factorPL * ( SC - minSC ) ) + minPL, MidpointRounding.AwayFromZero );
        int minPLValue = (int)Math.Round( maxPLValue * 0.55f, MidpointRounding.AwayFromZero );

        float factorAT = ( maxAT - minAT ) / ( maxSC - minSC );
        int maxATValue = (int)Math.Round( ( factorAT * ( SC - minSC ) ) + minAT, MidpointRounding.AwayFromZero );
        int minATValue = (int)Math.Round( maxATValue * 0.65f, MidpointRounding.AwayFromZero );

        float factorEL = ( maxEL - minEL ) / ( maxSC - minSC );
        int maxELValue = (int)Math.Round( ( factorEL * ( SC - minSC ) ) + minEL, MidpointRounding.AwayFromZero );
        int minELValue = (int)Math.Round( maxELValue * 0.75f, MidpointRounding.AwayFromZero );

        float factorST = ( maxST - minST ) / ( maxSC - minSC );
        int maxSTValue = (int)Math.Round( ( factorST * ( SC - minSC ) ) + minST, MidpointRounding.AwayFromZero );
        int minSTValue = (int)Math.Round( maxSTValue * 0.55f, MidpointRounding.AwayFromZero );



        if( SC < gameModel.User.SC )
        {
            spendAtomsToUseSC( SC );

            GameObject solarPrefabInstance = Instantiate( solarPrefab, galaxy.transform );
            SolarComponent solar = solarPrefabInstance.GetComponent<SolarComponent>();

            SolarModel solarModel = new SolarModel();
            solarModel.Radius = (int)( SC * 0.6f );
            solarModel.Lifetime = (int)( ( factorLT * ( SC - minSC ) ) + minLT );

            //Refactor: planetmanager should handle this
            int PlanetsLength = UnityEngine.Random.Range( minPLValue, maxPLValue );
            solarModel.Planets = new List<PlanetModel>();

            for( int index = 0; index < PlanetsLength; index++ )
            {
                PlanetModel planetModel = new PlanetModel();
                planetModel.Name = "Planet " + index;
                planetModel.Radius = (int)( SC * 0.4f ) / PlanetsLength;

                int atomsAvailable = UnityEngine.Random.Range( minATValue, maxATValue );
                PlanetAtomModel planetAtomModel;
                for( int atomIndex = 0; atomIndex < atomsAvailable; atomIndex++ )
                {
                    planetAtomModel = new PlanetAtomModel();
                    planetAtomModel.AtomicNumber = UnityEngine.Random.Range( minELValue, maxELValue );
                    planetAtomModel.Stock = UnityEngine.Random.Range( minSTValue, maxSTValue );
                    planetAtomModel.HarvestRate = 1;
                    planetAtomModel.UpgradeLevel = 0;
                    planetModel.Atoms[ planetAtomModel.AtomicNumber ] = planetAtomModel;
                }

                solarModel.Planets.Add( planetModel );
            }
            //EndRefactor: planetmanager should handle this

            solars.Add( solar );
            gameModel.User.Galaxies.Add( solarModel );
            solar.Setup( solarModel );
            Messenger.Dispatch( SolarMessage.SOLAR_CREATED );
        }
        else
        {
            Debug.Log( "Not enough SC to create Solar system!" );
        }

        
        
    }

    private void spendAtomsToUseSC( float SC )
    {
        float _sc = SC;

        foreach( KeyValuePair<int, AtomModel> atomModel in gameModel.User.Atoms )
        {
            int delta = 0;
            //Debug.Log( "removing atom: " + atomModel.Value.Symbol + " || Stock: " + atomModel.Value.Stock);
            for( int atomIndex = atomModel.Value.Stock; atomIndex > 0; atomIndex-- )
            {
                if( _sc - atomModel.Value.AtomicWeight >= 0 )
                {
                    _sc -= atomModel.Value.AtomicWeight;
                    delta++;
                }
                else
                {
                    break;
                }
            }
            Messenger.Dispatch( AtomMessage.ATOM_STOCK_CHANGED, new AtomMessage( atomModel.Key, -delta ) );
            //Debug.Log( "Stock after change:" + gameModel.User.Atoms[ atomModel.Key ].Stock );
        }
    }
}
