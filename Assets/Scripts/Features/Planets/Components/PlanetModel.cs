using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class PlanetModel
{
    public string Name = "DefaultPlanet";
    public int Radius = 10;
    public int Distance = 10;
    public int[] AtomsAvailable;
    public int[] AtomsStock;
    public int[] AtomsHarvestRates;
    public int[] AtomsUpgradeLevels;
}
