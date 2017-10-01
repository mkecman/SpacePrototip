using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class UserConfig
{
    public UserModel Data;
    private string jsonFilePath;
    
    public UserConfig()
    {
        jsonFilePath = Application.persistentDataPath + "-User.json";
    }

    public void Load()
    {
        if (File.Exists(jsonFilePath))
        {
            Data = JsonUtility.FromJson<UserModel>( File.ReadAllText( jsonFilePath ) );
        }
    }

    public void Save()
    {
        File.WriteAllText( jsonFilePath, JsonUtility.ToJson( Data, true ) );
    }
    
}