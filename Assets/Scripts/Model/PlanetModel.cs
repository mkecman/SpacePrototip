using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class PlanetModel
{
    public string Name = "DefaultPlanet";
    public int Radius = 10;
    public int Distance = 10;
    public int[] AtomsAvailable = new int[] { 1, 2, 3 };
    public int[] AtomsStock = new int[] { 10, 20, 30 };
    public int[] AtomsHarvestRates = new int[] { 10, 20, 30 };
    public int[] AtomsUpgradeLevels = new int[] { 0, 0, 0 };
}
