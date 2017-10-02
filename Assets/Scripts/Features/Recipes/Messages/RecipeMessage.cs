using UnityEngine;
using System.Collections;

public class RecipeMessage : AbstractMessage
{
    public static string CRAFT_COMPOUND_REQUEST = "CraftCompoundRequest";

    public RecipeModel Model;
    public float Amount;

    public RecipeMessage( RecipeModel model, float amount )
    {
        Model = model;
        Amount = amount;
    }
}
