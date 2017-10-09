using System;
using System.Collections.Generic;
using UniRx;

[Serializable]
public class JSONUserModel
{
    public string ID;
    public int XP;
    public float SC;
    public int HC;
    public int StarsCreated;
    public int PlanetsCreated;
    public List<JSONAtomModel> Atoms;
    public List<JSONSolarModel> Galaxies;
}