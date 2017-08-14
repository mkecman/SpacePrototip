using System;
using UnityEngine;

public class UserConfig
{
    public string userJSONString = "{  \"ID\": \"1\",  \"XP\": 1,  \"SC\": 0,  \"HC\": 0,  \"AtomsUnlocked\": 3,  \"AtomsStock\": [    0,    10,    40  ],  \"AtomsMax\": [    100,    150,    250  ], \"Galaxies\": []}";
    public UserModel Data;

    public UserConfig()
    {
        Data = JsonUtility.FromJson<UserModel>( userJSONString );
    }
}