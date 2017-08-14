using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

public class PlanetMessage : AbstractMessage
{
    public static string CREATE_PLANET = "CreatePlanet";

    public List<PlanetModel> Planets;
    public Transform Container;

    public PlanetMessage( List<PlanetModel> planets, Transform container )
    {
        Planets = planets;
        Container = container;
    }
}