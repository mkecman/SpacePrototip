﻿using System;
using System.Collections.Generic;

[Serializable]
public class UserModel
{
    public string ID;
    public int XP;
    public float SC;
    public int HC;
    public int AtomsUnlocked;
    public Dictionary<int, AtomModel> Atoms;
    public List<SolarModel> Galaxies;
}