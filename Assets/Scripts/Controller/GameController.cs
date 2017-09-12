using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : AbstractController
{
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
    }
    
    public void GenerateAtom()
    {
        Messenger.Dispatch( AtomMessage.GENERATE_ATOM );
        gameModel.SaveUser();
    }

    
}