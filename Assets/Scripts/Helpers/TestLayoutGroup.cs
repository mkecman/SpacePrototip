using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestLayoutGroup : LayoutGroup
{
    public override void CalculateLayoutInputVertical()
    {
        Debug.Log("CalculateLayoutInputVertical");
    }

    public override void SetLayoutHorizontal()
    {
        Debug.Log("SetLayoutHorizontal");
    }

    public override void SetLayoutVertical()
    {
        Debug.Log("SetLayoutVertical");
    }

    
}
