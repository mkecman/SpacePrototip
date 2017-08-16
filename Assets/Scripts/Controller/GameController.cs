using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : AbstractController
{
    public Slider SCSlider;
    public Text SCText;

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
        StartCoroutine( ExecuteAfterTime(1) );
    }

    // called third
    void Start()
    {
        //Debug.Log( "Start" );
    }

    // called when the game is terminated
    void OnDisable()
    {
        //Debug.Log( "OnDisable" );
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    IEnumerator ExecuteAfterTime( float time )
    {
        yield return new WaitForSeconds( time );

        Setup();
    }

    public void Setup()
    {
        Messenger.Dispatch( GameMessage.MODEL_LOADED );
        Messenger.Dispatch( AtomMessage.SETUP_ATOMS );
    }
    
    public void GenerateAtom()
    {
        Messenger.Dispatch( AtomMessage.GENERATE_ATOM );
    }

    public void CreateSolar()
    {
        Messenger.Dispatch( SolarMessage.CREATE_SOLAR, new SolarMessage( SCSlider.value ) );
    }

    public void UpdateSCLabel()
    {
        SCText.text = SCSlider.value + "";
    }
}