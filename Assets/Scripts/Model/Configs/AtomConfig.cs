using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class AtomConfig
{
    public AtomModel[] Data;
    private string jsonFilePath;

    public AtomConfig()
    {
        jsonFilePath = Application.persistentDataPath + "atoms.json";
        Debug.Log(jsonFilePath);
    }

    internal void Load()
    {
        if( File.Exists( jsonFilePath ) )
        {
            Data = JsonHelper.FromJson<AtomModel>( File.ReadAllText(jsonFilePath) );
        }
    }

    internal void Save()
    {
        File.WriteAllText(jsonFilePath, JsonHelper.ToJson<AtomModel>(Data));
    }
}