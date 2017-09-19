﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class UserConfig
{
    public string userJSONString = "{  \"ID\": \"1\",  \"XP\": 1,  \"SC\": 0,  \"HC\": 0,  \"AtomsUnlocked\": 18,  \"Atoms\": {    \"0\": {      \"Stock\": 0,      \"MaxStock\": 0    }, \"1\": {      \"Stock\": 20,      \"MaxStock\": 20    }, \"2\": {      \"Stock\": 10,      \"MaxStock\": 20    }  },  \"Galaxies\": []}";
    public UserModel Data;
    private string jsonFilePath;
    BinaryFormatter bf;

    public UserConfig()
    {
        jsonFilePath = Application.persistentDataPath + "-User.dat";
        bf = new BinaryFormatter();
    }

    public void Load()
    {
        if (File.Exists(jsonFilePath))
        {
            FileStream file = File.Open(jsonFilePath, FileMode.Open);
            Data = (UserModel)bf.Deserialize(file);
            file.Close();
        }
    }

    public void Save()
    {
        Debug.Log(JsonUtility.ToJson(Data));
        FileStream file = File.Create(jsonFilePath);
        bf.Serialize(file, Data);
        file.Close();
    }

    public UserModel getUser( AtomModel[] atomsDef )
    {
        UserModel user = new UserModel();
        JSONObject raw = new JSONObject( userJSONString );

        user.ID = raw[ "ID" ].str;
        user.XP = (int) raw[ "XP" ].n;
        user.SC = raw[ "SC" ].n;
        user.HC = (int) raw[ "HC" ].n;
        user.AtomsUnlocked = (int) raw[ "AtomsUnlocked" ].n;

        user.Atoms = new List<AtomModel>();
        user.Atoms.Add(new AtomModel()); //adding dummy non-existent zero atomic number object
        var rawAtoms = raw[ "Atoms" ];

        for( int atomicNumber = 1; atomicNumber <= user.AtomsUnlocked; atomicNumber++ )
        {
            AtomModel atomDef = atomsDef[ atomicNumber ];
            AtomModel atomModel = new AtomModel();

            atomModel.AtomicNumber = atomicNumber;
            atomModel.AtomicWeight = atomDef.AtomicWeight;
            atomModel.Name = atomDef.Name;
            atomModel.Symbol = atomDef.Symbol;
            if( rawAtoms.Count > atomicNumber )
            {
                atomModel.Stock = (int)rawAtoms[ atomicNumber ][ "Stock" ].n;
                atomModel.MaxStock = (int)rawAtoms[ atomicNumber ][ "MaxStock" ].n;
            }
            else
            {
                atomModel.Stock = 0;
                atomModel.MaxStock = 1;
                
            }
            
            user.Atoms.Insert( atomicNumber, atomModel );
        }

        user.Galaxies = new List<SolarModel>();

        Data = user;

        return user;
    }
}