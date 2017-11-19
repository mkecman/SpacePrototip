using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections.Generic;
using UniRx;

public class PlanetaryEnvironmentConfig
{
    public ReactiveCollection<PlanetaryEnvironmentModel> rxCollection;
    public List<JSONPlanetaryEnvironmentModel> jsonModel;
    public Dictionary<string, PlanetaryEnvironmentModel> rxBySymbol;

    internal void Load()
    {
        TextAsset targetFile = Resources.Load<TextAsset>("Configs/PlanetaryEnvironmentConfig");
        jsonModel = JsonHelper.FromJsonList<JSONPlanetaryEnvironmentModel>(targetFile.text);
        createRxAtoms();
        storeAtomsBySymbol();
    }

    private void createRxAtoms()
    {
        rxCollection = new ReactiveCollection<PlanetaryEnvironmentModel>();
        for (int i = 0; i < jsonModel.Count; i++)
        {
            rxCollection.Add(new PlanetaryEnvironmentModel( jsonModel[i]));
        }
    }

    private void storeAtomsBySymbol()
    {
        rxBySymbol = new Dictionary<string, PlanetaryEnvironmentModel>();
        for (int i = 0; i < jsonModel.Count; i++)
        {
            rxBySymbol.Add(jsonModel[i].Symbol, rxCollection[i]);
        }
    }

    internal void Save()
    {
        //File.WriteAllText(jsonFilePath, JsonHelper.ToJson<AtomModel>(Data));
    }
}

