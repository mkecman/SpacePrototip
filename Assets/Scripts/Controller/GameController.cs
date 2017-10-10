using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UniRx;

public class GameController : AbstractController
{
    public GameObject CraftingPanel;

    // called zero
    void Awake()
    {
        //Debug.Log( "Awake" );
    }

    // called first
    void OnEnable()
    {
        //Debug.Log( "OnEnable called" );
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded( Scene scene, LoadSceneMode mode )
    {
        //Debug.Log( "OnSceneLoaded: " + scene.name );
        //Debug.Log( mode );
        StartCoroutine(DelayedSetup(1) );
    }

    // called third
    void Start()
    {
        //Debug.Log( "Start" );
    }

    // called when the game is terminated
    void OnDisable()
    {
        Debug.Log( "OnDisable" );
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    IEnumerator DelayedSetup( float time )
    {
        yield return new WaitForSeconds( time );

        Messenger.Listen(GameMessage.MODEL_LOADED, Setup);

        gameModel.Init();
    }

    public void Setup( AbstractMessage message )
    {
        Messenger.Dispatch( AtomMessage.SETUP_ATOMS );

        CraftingPanel.SetActive(false);

        /*
        GenerateAtom();
        GenerateAtom();
        GenerateAtom();
        GenerateAtom();
        */
    }

    public void ShowHideCrafting()
    {
        if( CraftingPanel.activeSelf )
            CraftingPanel.SetActive( false );
        else
            CraftingPanel.SetActive( true );
        
    }

    public void SaveUser()
    {
        gameModel.SaveUser();
    }
    
    public void GenerateAtom( int times = 1 )
    {
        for( int i = 0; i < times; i++ )
        {
            Messenger.Dispatch( AtomMessage.GENERATE_ATOM );
        }
        
        //gameModel.SaveUser();
    }

    
}