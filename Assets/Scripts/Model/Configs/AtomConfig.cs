using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections.Generic;

public class AtomConfig
{
    public AtomModel[] Data;
    public Dictionary<string, AtomModel> AtomsBySymbol;
    private string jsonFilePath;

    public AtomConfig()
    {
        jsonFilePath = Application.persistentDataPath + "atoms.json";
    }

    internal void Load()
    {
        TextAsset targetFile = Resources.Load<TextAsset>( "Configs/Atoms" );
        Data = JsonHelper.FromJson<AtomModel>( targetFile.text );
        storeAtomsBySymbol();
    }

    private void storeAtomsBySymbol()
    {
        AtomsBySymbol = new Dictionary<string, AtomModel>();
        for (int i = 0; i < Data.Length; i++)
        {
            AtomsBySymbol.Add(Data[i].Symbol, Data[i]);
        }
    }

    internal void Save()
    {
        //File.WriteAllText(jsonFilePath, JsonHelper.ToJson<AtomModel>(Data));
    }
}