using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResistanceChartPoint : MonoBehaviour
{
    public Text Value;

    public void Setup( float value )
    {
        Value.text = value.ToString();
    }
}
