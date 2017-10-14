using System.Collections.Generic;
using UnityEngine;

public class GameConfig
{
    public int MaxAtomicNumber = 20;
    public float curve = 6f;
    public float MaxHarvestMultiplier = 3f;
    public float HarvestRateUpgradeStep = 0.2f;
    
    public Color32 GreenColor = new Color32( 0, 120, 20, 255 );

    public float minSC = 8f;
    public float maxSC = 86000f;

    public bool autoPlay = false;

    public Dictionary<int, int> atomSCUnlockRanges;

    public GameConfig()
    {
        atomSCUnlockRanges = new Dictionary<int, int>();
        atomSCUnlockRanges[ 1 ] = 0;
        atomSCUnlockRanges[ 2 ] = 101;
        atomSCUnlockRanges[ 3 ] = 652;
        atomSCUnlockRanges[ 4 ] = 1797;
        atomSCUnlockRanges[ 5 ] = 3444;
        atomSCUnlockRanges[ 6 ] = 5513;
        atomSCUnlockRanges[ 7 ] = 7914;
        atomSCUnlockRanges[ 8 ] = 10686;
        atomSCUnlockRanges[ 9 ] = 13839;
        atomSCUnlockRanges[ 10 ] = 17482;
        atomSCUnlockRanges[ 11 ] = 21463;
        atomSCUnlockRanges[ 12 ] = 25919;
        atomSCUnlockRanges[ 13 ] = 30721;
        atomSCUnlockRanges[ 14 ] = 35988;
        atomSCUnlockRanges[ 15 ] = 41576;
        atomSCUnlockRanges[ 16 ] = 47643;
        atomSCUnlockRanges[ 17 ] = 54030;
        atomSCUnlockRanges[ 18 ] = 60947;
        atomSCUnlockRanges[ 19 ] = 68530;
        atomSCUnlockRanges[ 20 ] = 76280;
    }
}
