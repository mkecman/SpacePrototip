using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class GalaxyModel
{
    public string Name = "DefaultGalaxy";
    public float Boost = 0.0f;
    public int[] Position = new int[] { 1, 1, 1 };
    public object[] Solars;
}
