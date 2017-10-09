using System;
using System.Collections.Generic;
using UniRx;

[Serializable]
public class UserModel
{
    public string ID;
    public int XP;
    //public float rSCValue;
    public FloatReactiveProperty rSC = new FloatReactiveProperty();
    public int HC;
    public int StarsCreated;
    public int PlanetsCreated;
    public List<AtomModel> Atoms;
    public List<SolarModel> Galaxies;
}