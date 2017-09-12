using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class AtomConfig
{
    public string atomsJSONString = "{   \"Items\":   [ {   \"Name\": \"null\",   \"Symbol\": \"\",   \"AtomicNumber\": 0,   \"AtomicWeight\": 0 }, {   \"Name\": \"Hydrogen\",   \"Symbol\": \" H\",   \"AtomicNumber\": 1,   \"AtomicWeight\": 1.00794 }, {   \"Name\": \"Helium\",   \"Symbol\": \" He\",   \"AtomicNumber\": 2,   \"AtomicWeight\": 4.002602 }, {   \"Name\": \"Lithium\",   \"Symbol\": \" Li\",   \"AtomicNumber\": 3,   \"AtomicWeight\": 6.941 }, {   \"Name\": \"Beryllium\",   \"Symbol\": \" Be\",   \"AtomicNumber\": 4,   \"AtomicWeight\": 9.01218 }, {   \"Name\": \"Boron\",   \"Symbol\": \" B\",   \"AtomicNumber\": 5,   \"AtomicWeight\": 10.811 }, {   \"Name\": \"Carbon\",   \"Symbol\": \" C\",   \"AtomicNumber\": 6,   \"AtomicWeight\": 12.011 }, {   \"Name\": \"Nitrogen\",   \"Symbol\": \" N\",   \"AtomicNumber\": 7,   \"AtomicWeight\": 14.00674 }, {   \"Name\": \"Oxygen\",   \"Symbol\": \" O\",   \"AtomicNumber\": 8,   \"AtomicWeight\": 15.9994 }, {   \"Name\": \"Fluorine\",   \"Symbol\": \" F\",   \"AtomicNumber\": 9,   \"AtomicWeight\": 18.998403 }, {   \"Name\": \"Neon\",   \"Symbol\": \" Ne\",   \"AtomicNumber\": 10,   \"AtomicWeight\": 20.1797 }, {   \"Name\": \"Sodium\",   \"Symbol\": \" Na\",   \"AtomicNumber\": 11,   \"AtomicWeight\": 22.989768 }, {   \"Name\": \"Magnesium\",   \"Symbol\": \" Mg\",   \"AtomicNumber\": 12,   \"AtomicWeight\": 24.305 }, {   \"Name\": \"Aluminum\",   \"Symbol\": \" Al\",   \"AtomicNumber\": 13,   \"AtomicWeight\": 26.981539 }, {   \"Name\": \"Silicon\",   \"Symbol\": \" Si\",   \"AtomicNumber\": 14,   \"AtomicWeight\": 28.0855 }, {   \"Name\": \"Phosphorus\",   \"Symbol\": \" P\",   \"AtomicNumber\": 15,   \"AtomicWeight\": 30.973762 }, {   \"Name\": \"Sulfur\",   \"Symbol\": \" S\",   \"AtomicNumber\": 16,   \"AtomicWeight\": 32.066 }, {   \"Name\": \"Chlorine\",   \"Symbol\": \" Cl\",   \"AtomicNumber\": 17,   \"AtomicWeight\": 35.4527 }, {   \"Name\": \"Argon\",   \"Symbol\": \" Ar\",   \"AtomicNumber\": 18,   \"AtomicWeight\": 39.948 }, {   \"Name\": \"Potassium\",   \"Symbol\": \" K\",   \"AtomicNumber\": 19,   \"AtomicWeight\": 39.0983 }, {   \"Name\": \"Calcium\",   \"Symbol\": \" Ca\",   \"AtomicNumber\": 20,   \"AtomicWeight\": 40.078 }]}";

    public AtomModel[] Data;
    private string jsonFilePath;

    public AtomConfig()
    {
        jsonFilePath = Application.persistentDataPath + "atoms.json";
        Debug.Log(jsonFilePath);
    }

    internal void Load()
    {
        Data = JsonHelper.FromJson<AtomModel>( atomsJSONString );

        /*
        if( File.Exists( jsonFilePath ) )
        {
            Data = JsonHelper.FromJson<AtomModel>( File.ReadAllText(jsonFilePath) );
        }
        */
    }

    internal void Save()
    {
        File.WriteAllText(jsonFilePath, JsonHelper.ToJson<AtomModel>(Data));
    }
}