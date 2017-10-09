using System;
using System.Collections.Generic;

[Serializable]
public class JSONSolarModel
{
    public string Name = "DefaultStar";
    public int Radius = 10;
    public int Lifetime = 30;
    public float CreatedSC = 10f;
    public List<JSONPlanetModel> Planets = new List<JSONPlanetModel>();
}
