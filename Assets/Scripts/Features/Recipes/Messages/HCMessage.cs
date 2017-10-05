using UnityEngine;
using System.Collections;

public class HCMessage : AbstractMessage
{
    public static string UPDATE_REQUEST = "HCUpdateRequest";
    public static string UPDATED = "HCUpdated";

    public int Amount;

    public HCMessage( int amount )
    {
        Amount = amount;
    }
}
