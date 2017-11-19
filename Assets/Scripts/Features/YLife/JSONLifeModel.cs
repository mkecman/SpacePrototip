using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JSONLifeModel
{
    public string Name;
    
    public float Population;
    public float PopulationGrowth;
    public float Science;
    public float ScienceGrowth;
    public float Farmers;
    public float Scientists;
    public float Miners;

    public List<JSONLifeHarvestModel> Harvesters;
    public List<JSONLifeResistanceModel> Resistance;
}
