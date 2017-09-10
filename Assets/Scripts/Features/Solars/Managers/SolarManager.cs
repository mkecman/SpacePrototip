﻿using UnityEngine;
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

        float minSC = 20;
        float maxSC = 5000;
        float minLT = 30;
        float maxLT = 240;
        float minPL = 1;
        float maxPL = 5;
        float minAT = 1;
        float maxAT = 4;
        float minEL = 1;
        float maxEL = gameModel.User.AtomsUnlocked;
        float minST = minLT * 10.0f;
        float maxST = maxLT * 10.0f;

        float factorLT = ( maxLT - minLT ) / ( maxSC - minSC );

        float factorPL = ( maxPL - minPL ) / ( maxSC - minSC );
        int maxPLValue = (int)Math.Round( ( factorPL * ( SC - minSC ) ) + minPL, MidpointRounding.AwayFromZero );
        int minPLValue = (int)Math.Round( maxPLValue * 0.55f, MidpointRounding.AwayFromZero );

        float curve = 0.5f;

        Dictionary<int, float> pmPL = new Dictionary<int, float>();
        for( int i = 0; i < maxPL; i++ )
        {
            float bellValue = ( 1 / Mathf.Sqrt( 2 * Mathf.PI * curve ) ) * Mathf.Exp( -Mathf.Pow( maxPLValue - i, 2 ) / ( 2 * curve ) );
            pmPL.Add( i, bellValue );
        }

        float factorAT = ( maxAT - minAT ) / ( maxSC - minSC );
        int maxATValue = (int)Math.Round( ( factorAT * ( SC - minSC ) ) + minAT, MidpointRounding.AwayFromZero );
        int minATValue = (int)Math.Round( maxATValue * 0.65f, MidpointRounding.AwayFromZero );

        Dictionary<int, float> pmAT = new Dictionary<int, float>();
        for( int i = 0; i < maxAT; i++ )
        {
            float bellValue = ( 1 / Mathf.Sqrt( 2 * Mathf.PI * curve ) ) * Mathf.Exp( -Mathf.Pow( maxATValue - i, 2 ) / ( 2 * curve ) );
            pmAT.Add( i, bellValue );
        }

        float factorEL = ( maxEL - minEL ) / ( maxSC - minSC );
        int maxELValue = (int)Math.Round( ( factorEL * ( SC - minSC ) ) + minEL, MidpointRounding.AwayFromZero );
        //int minELValue = (int)Math.Round( maxELValue * 0.51f, MidpointRounding.AwayFromZero );

        Dictionary<int, float> pmEL = new Dictionary<int, float>();
        for( int i = 0; i < maxEL; i++ )
        {
            float bellValue = ( 1 / Mathf.Sqrt( 2 * Mathf.PI * curve ) ) * Mathf.Exp( - Mathf.Pow( maxELValue - i, 2 ) / ( 2 * curve ) );
            pmEL.Add( i, bellValue );
        }

        float factorST = ( maxST - minST ) / ( maxSC - minSC );
        int maxSTValue = (int)Math.Round( ( factorST * ( SC - minSC ) ) + minST, MidpointRounding.AwayFromZero );
        int minSTValue = (int)Math.Round( maxSTValue * 0.55f, MidpointRounding.AwayFromZero );

        Dictionary<int, float> pmST = new Dictionary<int, float>();
        for( int i = 0; i < maxST; i++ )
        {
            float bellValue = ( 1 / Mathf.Sqrt( 2 * Mathf.PI * curve ) ) * Mathf.Exp( -Mathf.Pow( maxSTValue - i, 2 ) / ( 2 * 0.5f ) );
            pmST.Add( i, bellValue );
        }

        if( SC < gameModel.User.SC )
        {
            spendAtomsToUseSC( SC );

            GameObject solarPrefabInstance = Instantiate( solarPrefab, galaxy.transform );
            SolarComponent solar = solarPrefabInstance.GetComponent<SolarComponent>();

            SolarModel solarModel = new SolarModel();
            solarModel.Radius = (int)( SC );
            solarModel.Lifetime = (int)( ( factorLT * ( SC - minSC ) ) + minLT );

            //Refactor: planetmanager should handle this
            //int PlanetsLength = (int)Choose( pmPL ) + 1;
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
                    planetAtomModel.AtomicNumber = (int)Choose( pmEL ) + 1;
                    Debug.Log( planetAtomModel.AtomicNumber );
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

    float Choose( Dictionary<int, float> probs )
    {

        float total = 0;

        foreach( KeyValuePair<int, float> elem in probs )
        {
            total += elem.Value;
        }

        float randomPoint = UnityEngine.Random.value * total;

        for( int i = 0; i < probs.Count; i++ )
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
        return probs.Count - 1;
    }

    private void spendAtomsToUseSC( float SC )
    {
        float _sc = SC;
        Dictionary<int, AtomModel> collectedAtoms = new Dictionary<int, AtomModel>();
        Dictionary<int, AtomMessage> atomsMessages = new Dictionary<int, AtomMessage>();

        foreach( KeyValuePair<int, AtomModel> atomModel in gameModel.User.Atoms )
        {
            if( atomModel.Value.Stock > 0 )
            {
                AtomModel atom = new AtomModel();
                atom.AtomicNumber = atomModel.Value.AtomicNumber;
                atom.AtomicWeight = atomModel.Value.AtomicWeight;
                atom.Stock = atomModel.Value.Stock;
                collectedAtoms.Add( atomModel.Key, atom );
                atomsMessages.Add( atomModel.Key, new AtomMessage( atomModel.Key, 0 ) );
            }
        }

        while( _sc > 0 )
        {
            foreach( KeyValuePair<int, AtomModel> atomModel in collectedAtoms )
            {
                if( atomModel.Value.Stock > 0 )
                {
                    if( _sc - atomModel.Value.AtomicWeight >= 0 )
                    {
                        _sc -= atomModel.Value.AtomicWeight;
                        atomModel.Value.Stock -= 1;
                        atomsMessages[ atomModel.Value.AtomicNumber ].Delta -= 1;
                    }
                    else
                    {
                        _sc = 0;
                        break;
                    }
                }
            }
        }

        foreach( KeyValuePair<int, AtomMessage> atomMessage in atomsMessages )
        {
            Messenger.Dispatch( AtomMessage.ATOM_STOCK_CHANGED, atomMessage.Value );
        }
    }
}
