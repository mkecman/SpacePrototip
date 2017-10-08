using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class SolarMessage : AbstractMessage
{
    public static string CREATE_SOLAR = "CreateSolar";
    public static string SOLAR_CREATED = "SolarCreated";
    public static string SOLAR_DESTROYED = "SolarDestroyed";

    public float SC;
    public float maxSC;
    public SolarModel model;

    public SolarMessage( float SC = 0, float maxSC = 0, SolarModel model = null )
    {
        this.SC = SC;
        this.maxSC = maxSC;
        this.model = model;
    }
}
