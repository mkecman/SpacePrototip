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
    }

    internal void Load()
    {
        TextAsset targetFile = Resources.Load<TextAsset>( "Configs/Atoms" );
        Data = JsonHelper.FromJson<AtomModel>( targetFile.text );
    }

    internal void Save()
    {
        //File.WriteAllText(jsonFilePath, JsonHelper.ToJson<AtomModel>(Data));
    }
}