using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class UserConfig
{
    public UserModel userModel;
    private JSONUserModel Data;
    private string jsonFilePath;
    
    public UserConfig()
    {
        jsonFilePath = Application.persistentDataPath + "-User.json";
    }

    public void Load()
    {
        if (File.Exists(jsonFilePath))
        {
            Data = JsonUtility.FromJson<JSONUserModel>( File.ReadAllText( jsonFilePath ) );
            userModel = new UserModel( Data );
        }
        else
        {
            TextAsset targetFile = Resources.Load<TextAsset>("Configs/DefaultUser");
            Data = JsonUtility.FromJson<JSONUserModel>(targetFile.text);
            Save();
        }
    }

    public void Save()
    {
        File.WriteAllText( jsonFilePath, JsonUtility.ToJson( Data, true ) );
    }
    
    
}