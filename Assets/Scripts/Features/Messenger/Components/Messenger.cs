using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;

public class Messenger : MonoBehaviour
{

    private Dictionary<string, ObjUnityEvent> eventDictionary;

    private static Messenger eventManager;

    public static Messenger instance
    {
        get
        {
            if( !eventManager )
            {
                eventManager = FindObjectOfType( typeof( Messenger ) ) as Messenger;

                if( !eventManager )
                {
                    Debug.LogError( "There needs to be one active Messenger script on a GameObject in your scene." );
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if( eventDictionary == null )
        {
            eventDictionary = new Dictionary<string, ObjUnityEvent>();
        }
    }

    public static void Listen( string type, UnityAction<AbstractMessage> listener )
    {
        ObjUnityEvent thisEvent = null;
        if( instance.eventDictionary.TryGetValue( type, out thisEvent ) )
        {
            thisEvent.AddListener( listener );
        }
        else
        {
            thisEvent = new ObjUnityEvent();
            thisEvent.AddListener( listener );
            instance.eventDictionary.Add( type, thisEvent );
        }
    }

    public static void StopListening( string type, UnityAction<AbstractMessage> listener )
    {
        if( eventManager == null ) return;
        ObjUnityEvent thisEvent = null;
        if( instance.eventDictionary.TryGetValue( type, out thisEvent ) )
        {
            thisEvent.RemoveListener( listener );
        }
    }
    
    public static void Dispatch( string type, AbstractMessage message = null )
    {
        ObjUnityEvent thisEvent = null;
        if( instance.eventDictionary.TryGetValue( type, out thisEvent ) )
        {
            thisEvent.Invoke( message );
        }
    }

    internal static void Dispatch( object aTOM_STOCK_UPDATED )
    {
        throw new NotImplementedException();
    }
}

[System.Serializable]
public class ObjUnityEvent : UnityEvent<AbstractMessage>
{
    
}
