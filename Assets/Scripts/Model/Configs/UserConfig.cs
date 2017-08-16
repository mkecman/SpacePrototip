using System;
using System.Collections.Generic;
using UnityEngine;

public class UserConfig
{
    public string userJSONString = "{  \"ID\": \"1\",  \"XP\": 1,  \"SC\": 0,  \"HC\": 0,  \"AtomsUnlocked\": 3,  \"Atoms\": {    \"1\": {      \"Stock\": 10,      \"MaxStock\": 100    },    \"2\": {      \"Stock\": 20,      \"MaxStock\": 100    },    \"3\": {      \"Stock\": 30,      \"MaxStock\": 100    }  },  \"Galaxies\": []}";
    public UserModel Data;

    public UserConfig()
    {
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

        user.Atoms = new Dictionary<int, AtomModel>();
        var rawAtoms = raw[ "Atoms" ];
        for( int i = 0; i < rawAtoms.Count; i++ )
        {
            int atomicNumber = int.Parse( rawAtoms.keys[ i ] );
            AtomModel atomDef = atomsDef[ atomicNumber ];
            AtomModel atomModel = new AtomModel();

            atomModel.AtomicNumber = atomicNumber;
            atomModel.AtomicWeight = atomDef.AtomicWeight;
            atomModel.Name = atomDef.Name;
            atomModel.Symbol = atomDef.Symbol;
            atomModel.Stock = (int)rawAtoms[ i ]["Stock"].n;
            atomModel.MaxStock = (int)rawAtoms[ i ]["MaxStock"].n;

            user.Atoms[ atomicNumber ] = atomModel;
        }

        user.Galaxies = new List<SolarModel>();

        return user;
    }
}