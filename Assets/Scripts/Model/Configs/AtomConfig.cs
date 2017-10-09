using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections.Generic;
using UniRx;

public class AtomConfig
{
    public ReactiveCollection<AtomModel> atoms;
    public List<JSONAtomModel> jsonModel;
    public Dictionary<string, AtomModel> AtomsBySymbol;
    private string jsonFilePath;

    public AtomConfig()
    {
        jsonFilePath = Application.persistentDataPath + "atoms.json";
    }

    internal void Load()
    {
        TextAsset targetFile = Resources.Load<TextAsset>( "Configs/Atoms" );
        jsonModel = JsonHelper.FromJsonList<JSONAtomModel>( targetFile.text );
        createRxAtoms();
        storeAtomsBySymbol();
    }

    private void createRxAtoms()
    {
        atoms = new ReactiveCollection<AtomModel>();
        for( int i = 0; i < jsonModel.Count; i++ )
        {
            atoms.Add( new AtomModel( jsonModel[ i ] ) );
        }
    }

    private void storeAtomsBySymbol()
    {
        AtomsBySymbol = new Dictionary<string, AtomModel>();
        for (int i = 0; i < jsonModel.Count; i++)
        {
            AtomsBySymbol.Add(jsonModel[i].Symbol, atoms[i]);
        }
    }

    internal void Save()
    {
        //File.WriteAllText(jsonFilePath, JsonHelper.ToJson<AtomModel>(Data));
    }
}