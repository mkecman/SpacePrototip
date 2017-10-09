using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class JSONPlanetModel
{
    public string Name = "DefaultPlanet";
    public int Radius = 10;
    public int Distance = 10;
    public List<JSONAtomModel> Atoms = new List<JSONAtomModel>();
    
}
