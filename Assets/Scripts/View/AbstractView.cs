using UnityEngine;
using System;
using System.Collections.Generic;

public class AbstractView : MonoBehaviour
{
    protected GameModel gameModel = GameModel.instance;

    protected List<IDisposable> reactors = new List<IDisposable>();

    protected void AddReactor( IDisposable reactor )
    {
        reactors.Add( reactor );
    }

    protected void DestroyReactors()
    {
        for( int i = 0; i < reactors.Count; i++ )
        {
            reactors[ i ].Dispose();
            reactors[ i ] = null;
        }

        reactors.Clear();
    }
}
