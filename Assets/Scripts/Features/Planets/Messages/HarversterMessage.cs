using UnityEngine;
using System.Collections;

public class HarvesterMessage : AbstractMessage
{
    public static string HARVESTER_UPGRADE = "HarvesterUpgrade";

    public AtomModel Model;

    public HarvesterMessage( AtomModel model )
    {
        Model = model;
    }
}
