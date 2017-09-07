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
        float SC = ( message as SolarMessage).SC;
        if( SC < gameModel.User.SC )
        {
            gameModel.User.SC -= SC;

            GameObject solarPrefabInstance = Instantiate( solarPrefab, galaxy.transform );
            SolarComponent solar = solarPrefabInstance.GetComponent<SolarComponent>();

            SolarModel solarModel = new SolarModel();
            solarModel.Radius = (int)( SC * 0.6f );
            solarModel.Lifetime = ( solarModel.Radius / 6 ) * 10;

            //Refactor: planetmanager should handle this
            int PlanetsLength = 3;
            solarModel.Planets = new List<PlanetModel>();

            for( int index = 0; index < PlanetsLength; index++ )
            {
                PlanetModel planetModel = new PlanetModel();
                planetModel.Radius = (int)( SC * 0.4f ) / PlanetsLength;

                int atomsAvailable = UnityEngine.Random.Range( 1, 5 );
                PlanetAtomModel planetAtomModel;
                for( int atomIndex = 0; atomIndex < atomsAvailable; atomIndex++ )
                {
                    planetAtomModel = new PlanetAtomModel();
                    planetAtomModel.AtomicNumber = UnityEngine.Random.Range( 1, gameModel.User.AtomsUnlocked + 1 );
                    planetAtomModel.Stock = UnityEngine.Random.Range( 1, 100 );
                    planetAtomModel.HarvestRate = UnityEngine.Random.Range( 1, 3 );
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
}
