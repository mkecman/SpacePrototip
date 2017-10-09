using System;
using System.Collections.Generic;

[Serializable]
public class SolarModel
{
    public string Name = "DefaultStar";
    public int Radius = 10;
    public int Lifetime = 30;
    public float CreatedSC = 10f;
    public List<PlanetModel> Planets = new List<PlanetModel>();
}
