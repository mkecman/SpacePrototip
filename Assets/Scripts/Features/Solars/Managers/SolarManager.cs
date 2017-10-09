using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class SolarManager : AbstractController
{
    public GameObject galaxy;
    public GameObject solarPrefab;

    private SolarModelManager SMM = new SolarModelManager();
    private int _layoutUpdateCount = 0;

    void Start()
    {
        Messenger.Listen( SolarMessage.CREATE_SOLAR, handleCreateSolar );
        Messenger.Listen(GameMessage.MODEL_LOADED, handleGameModelLoaded);
        Messenger.Listen( SolarMessage.SOLAR_DESTROYED, handleSolarDestroyed );
        SMM.Setup( gameModel );
    }
    
    private void handleCreateSolar( AbstractMessage message )
    {
        float SC = ( message as SolarMessage ).SC;
        float maxSC = (message as SolarMessage).maxSC;

        /**/
        if( SC > gameModel.User.rSC.Value )
        {
            Debug.Log( "Not enough SC to create Solar system!" );
            return;
        }

        Messenger.Dispatch( AtomMessage.DEDUCT_ATOMS_WORTH_SC, new AtomMessage( 0, 0, SC ) );
        /**/
        SolarModel generatedSolarModel = SMM.GenerateSolar( SC );
        gameModel.User.Galaxies.Add( generatedSolarModel );

        GameObject solarPrefabInstance = Instantiate( solarPrefab, galaxy.transform );
        SolarComponent solar = solarPrefabInstance.GetComponent<SolarComponent>();
        solar.Setup( generatedSolarModel );
        Messenger.Dispatch( SolarMessage.SOLAR_CREATED );

        _layoutUpdateCount = 0; //fix for layoutgroup update, check Update() function;
    }

    private void handleSolarDestroyed( AbstractMessage message )
    {
        gameModel.User.Galaxies.Remove( ( message as SolarMessage ).model );
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

    void Update()
    {
        if( _layoutUpdateCount < 30 )
        {
            LayoutRebuilder.MarkLayoutForRebuild( galaxy.transform as RectTransform );
            _layoutUpdateCount++;
        }
    }


}
