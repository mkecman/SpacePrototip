using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class RecipeConfig
{
    public List<RecipeModel> Data;

    internal void Load( Dictionary<string, AtomModel> Atoms )
    {
        TextAsset targetFile = Resources.Load<TextAsset>( "Configs/Recipes" );
        Data = JsonHelper.FromJsonList<RecipeModel>( targetFile.text );

        foreach( RecipeModel recipe in Data )
        {
            recipe.Setup( Atoms );
        }
    }
}