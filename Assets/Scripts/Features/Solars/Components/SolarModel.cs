using System;
using System.Collections.Generic;

[Serializable]
public class SolarModel
{
    public string Name = "DefaultStar";
    public int Radius = 10;
    public int Lifetime = 60;
    public List<PlanetModel> Planets = new List<PlanetModel>();
}
