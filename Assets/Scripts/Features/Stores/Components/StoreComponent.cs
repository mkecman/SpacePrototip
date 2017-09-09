using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StoreComponent : MonoBehaviour
{
    public Slider Fill;
    public Image FillImage;
    public Text UIName;
    public Text UIStock;
    public Text UIProperty;

    public string ID;
    private string _name;
    private string _property;
    private int _stock;
    private Color _originalColor;

    public int MaxStock
    {
        set
        {
            Fill.maxValue = value;
        }
    }

    public int Stock
    {
        set
        {
            _stock = value;
            Fill.value = value;
            UIStock.text = "" + value;
        }

        get
        {
            return _stock;
        }
    }

    public string Name
    {
        set
        {
            _name = value;
            UIName.text = _name;
        }
    }

    public string Property
    {
        set
        {
            _property = value;
            UIProperty.text = _property;
        }

        get
        {
            return _property;
        }
    }

    public void Blink()
    {
        _originalColor = FillImage.color;
        FillImage.color = Color.red;
        StartCoroutine( Unblink( 1 ) );
    }

    IEnumerator Unblink( float time )
    {
        yield return new WaitForSeconds( time );

        FillImage.color = _originalColor;
        Debug.Log( _originalColor.ToString() );
    }
}
