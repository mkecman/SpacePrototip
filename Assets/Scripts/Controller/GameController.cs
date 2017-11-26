using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UniRx;
using System;

public class GameController : AbstractController
{
    // called zero
    void Awake()
    {
        Debug.Log( "Awake" );
        DontDestroyOnLoad( gameObject );
        StartCoroutine( DelayedSetup( 1 ) );
    }

    // called first
    void OnEnable()
    {
        Debug.Log( "OnEnable called" );
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded( Scene scene, LoadSceneMode mode )
    {
        Debug.Log( "OnSceneLoaded: " + scene.name + " | Mode: " + mode.ToString() );
    }

    // called third
    void Start()
    {
        Debug.Log( "Start" );
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
        //LoadScene( "LifeSimulator" );
        Messenger.Dispatch( AtomMessage.SETUP_ATOMS );
    }
    
    public void SaveUser()
    {
        gameModel.SaveUser();
    }

    private void LoadScene( string scene, LoadSceneMode mode = LoadSceneMode.Single )
    {
        StartCoroutine( LoadSceneAsync( scene, mode ) );
    }

    IEnumerator LoadSceneAsync( string scene, LoadSceneMode mode )
    {
        // The Application loads the Scene in the background at the same time as the current Scene.
        //This is particularly good for creating loading screens. You could also load the Scene by build //number.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync( scene, mode );

        //Wait until the last operation fully loads to return anything
        while( !asyncLoad.isDone )
        {
            yield return null;
        }
    }



}