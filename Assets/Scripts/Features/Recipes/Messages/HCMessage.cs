using UnityEngine;
using System.Collections;

public class HCMessage : AbstractMessage
{
    public static string UPDATED = "HCUpdated";

    public int Amount;

    public HCMessage( int amount )
    {
        Amount = amount;
    }
}
