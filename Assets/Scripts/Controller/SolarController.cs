using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public class SolarController : AbstractController
{
    public GameObject galaxy;
    public GameObject solarPrefab;
    
    private List<SolarView> solarViews = new List<SolarView>();

    void Start()
    {
        Messenger.Listen( SolarMessage.CREATE_SOLAR, handleCreateSolar );
    }

    private void handleCreateSolar( AbstractMessage message )
    {
        float SC = ( message as SolarMessage).SC;

        GameObject solar = Instantiate( solarPrefab, galaxy.transform );
        SolarView solarView = solar.GetComponent<SolarView>();

        SolarModel solarModel = new SolarModel();
        solarModel.Radius = (int)( SC * 0.6f );
        solarModel.Lifetime = ( solarModel.Radius / 6 ) * 36;
        int PlanetsLength = 3;
        solarModel.Planets = new List<PlanetModel>();
        
        for( int index = 0; index < PlanetsLength; index++ )
        {
            PlanetModel planetModel = new PlanetModel();
            planetModel.Radius = (int)( SC * 0.4f ) / PlanetsLength;
            int atomsAvailable = UnityEngine.Random.Range( 1, 3 );

            planetModel.AtomsAvailable = new int[ atomsAvailable ];
            planetModel.AtomsStock = new int[ atomsAvailable ];
            planetModel.AtomsHarvestRates = new int[ atomsAvailable ];
            planetModel.AtomsUpgradeLevels = new int[ atomsAvailable ];

            for( int atomIndex = 0; atomIndex < atomsAvailable; atomIndex++ )
            {
                planetModel.AtomsAvailable[ atomIndex ] = UnityEngine.Random.Range( 1, gameModel.atomsCount );
                planetModel.AtomsStock[ atomIndex ] = UnityEngine.Random.Range( 1, 100 );
                planetModel.AtomsHarvestRates[ atomIndex ] = 1;
                planetModel.AtomsUpgradeLevels[ atomIndex ] = 0;
            }
            
            solarModel.Planets.Add( planetModel );
        }

        solarView.SetupView( solarModel );
        solarViews.Add( solarView );
        
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
