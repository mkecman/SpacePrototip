using UnityEngine;
using System.Collections;
using System;

public class AutoPlayer : MonoBehaviour
{
    private float lastTime;
    private bool _isActive;

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
        if( _isActive && Time.time - lastTime > 0.3f )
        {
            Messenger.Dispatch(SolarMessage.CREATE_SOLAR, new SolarMessage(10000));
            lastTime = Time.time;
        }
    }
}
