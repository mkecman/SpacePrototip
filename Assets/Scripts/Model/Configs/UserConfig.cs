using System;
using System.Collections.Generic;
using UnityEngine;

public class UserConfig
{
    public string userJSONString = "{  \"ID\": \"1\",  \"XP\": 1,  \"SC\": 0,  \"HC\": 0,  \"AtomsUnlocked\": 18,  \"Atoms\": {    \"0\": {      \"Stock\": 0,      \"MaxStock\": 0    }, \"1\": {      \"Stock\": 1000,      \"MaxStock\": 100    },    \"2\": {      \"Stock\": 2,      \"MaxStock\": 100    },    \"3\": {      \"Stock\": 3,      \"MaxStock\": 100    }  },  \"Galaxies\": []}";
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

        for( int atomicNumber = 1; atomicNumber <= user.AtomsUnlocked; atomicNumber++ )
        {
            AtomModel atomDef = atomsDef[ atomicNumber ];
            AtomModel atomModel = new AtomModel();

            atomModel.AtomicNumber = atomicNumber;
            atomModel.AtomicWeight = atomDef.AtomicWeight;
            atomModel.Name = atomDef.Name;
            atomModel.Symbol = atomDef.Symbol;
            atomModel.MaxStock = 100 - ( atomicNumber * 4 );
            if( rawAtoms.Count > atomicNumber )
            {
                atomModel.Stock = (int)rawAtoms[ atomicNumber ][ "Stock" ].n;
                //atomModel.MaxStock = (int)rawAtoms[ atomicNumber ][ "MaxStock" ].n;
            }
            else
            {
                atomModel.Stock = 0;
                
            }
            

            user.Atoms[ atomicNumber ] = atomModel;
        }

        user.Galaxies = new List<SolarModel>();

        return user;
    }
}