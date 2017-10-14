using UnityEngine;
using System.Collections;
using System;

public class AutoPlayer : AbstractController
{
    [SerializeField]
    private float _timeScale;

    private float _currentTimeScale;

    public int spawnEvery = 10;
    public int SC = 100;

    private SolarMessage message = new SolarMessage( 0 );
    private AtomMessage upgradeMessage = new AtomMessage( 1, 1 );
    
    private float lastTime;
    private bool _isActive = true;

    private void OnEnable()
    {
        gameModel.Config.autoPlay = true;
    }

    private void OnDisable()
    {
        gameModel.Config.autoPlay = false;
    }

    // Use this for initialization
    void Start()
    {
        Messenger.Listen(GameMessage.MODEL_LOADED, handleGameLoaded);
    }

    private void handleGameLoaded(AbstractMessage arg0)
    {
        _isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if( _timeScale != _currentTimeScale )
        {
            Time.timeScale = _timeScale;
            _currentTimeScale = _timeScale;
        }
        
        if( _isActive && Time.time - lastTime > spawnEvery )
        {
            for( int i = 0; i < gameModel.User.Atoms.Count; i++ )
            {
                AtomModel atom = gameModel.User.Atoms[ i ];
                if( atom.Stock == atom.MaxStock )
                {
                    upgradeMessage.AtomicNumber = atom.AtomicNumber;
                    Messenger.Dispatch( AtomMessage.ATOM_STOCK_UPGRADE, upgradeMessage );
                }
            }

            message.SC = gameModel.User.SC;
            Messenger.Dispatch( SolarMessage.CREATE_SOLAR, message );

            lastTime = Time.time;
        }
    }
}
