using System.Collections.Generic;
using UnityEngine;

public class GameConfig
{
    public int MaxAtomicNumber = 20;
    public float curve = 10f;
    public float MaxHarvestMultiplier = 3f;
    public float HarvestRateUpgradeStep = 0.2f;
    public float HarvestTimeSpan = 1f;

    public Color32 GreenColor = new Color32( 0, 120, 20, 255 );

    public float minSC = 8f;
    public float maxSC = 30000f;

    public bool autoPlay = false;

    public Dictionary<int, int> atomSCUnlockRanges;

    public GameConfig()
    {
        atomSCUnlockRanges = new Dictionary<int, int>();
        atomSCUnlockRanges[ 1 ] = 0;
        atomSCUnlockRanges[ 2 ] = 55;
        atomSCUnlockRanges[ 3 ] = 320;
        atomSCUnlockRanges[ 4 ] = 699;
        atomSCUnlockRanges[ 5 ] = 1557;
        atomSCUnlockRanges[ 6 ] = 2357;
        atomSCUnlockRanges[ 7 ] = 3271;
        atomSCUnlockRanges[ 8 ] = 4307;
        atomSCUnlockRanges[ 9 ] = 5468;
        atomSCUnlockRanges[ 10 ] = 6777;
        atomSCUnlockRanges[ 11 ] = 8215;
        atomSCUnlockRanges[ 12 ] = 9796;
        atomSCUnlockRanges[ 13 ] = 11508;
        atomSCUnlockRanges[ 14 ] = 13361;
        atomSCUnlockRanges[ 15 ] = 15342;
        atomSCUnlockRanges[ 16 ] = 17461;
        atomSCUnlockRanges[ 17 ] = 19709;
        atomSCUnlockRanges[ 18 ] = 22101;
        atomSCUnlockRanges[ 19 ] = 24662;
        atomSCUnlockRanges[ 20 ] = 27357;
    }
}
