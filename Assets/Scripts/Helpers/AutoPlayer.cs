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
            message.SC = gameModel.User.SC;
            Messenger.Dispatch(SolarMessage.CREATE_SOLAR, message);

            upgradeMessage.AtomicNumber = gameModel.User.Atoms.Count - 1;
            Messenger.Dispatch( AtomMessage.ATOM_STOCK_UPGRADE, upgradeMessage );

            lastTime = Time.time;
        }
    }
}
