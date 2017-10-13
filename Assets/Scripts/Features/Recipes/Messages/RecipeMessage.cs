using UnityEngine;
using System.Collections;

public class RecipeMessage : AbstractMessage
{
    public static string CRAFT_COMPOUND_REQUEST = "CraftCompoundRequest";

    public RecipeModel Model;
    public int Amount;

    public RecipeMessage( RecipeModel model, int amount )
    {
        Model = model;
        Amount = amount;
    }
}
