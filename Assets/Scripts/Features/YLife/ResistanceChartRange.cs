using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResistanceChartRange : MonoBehaviour
{
    public Text Min;
    public Text Max;
    
    public void Setup( float min, float max )
    {
        Min.text = min.ToString();
        Max.text = max.ToString();
    }
}
