using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class PlanetModel
{
    public string Name = "DefaultPlanet";
    public int Radius = 10;
    public int Distance = 10;
    public List<AtomModel> Atoms = new List<AtomModel>();
    
}
