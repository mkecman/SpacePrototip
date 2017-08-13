using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StoreView : MonoBehaviour
{
    public Slider Fill;
    public Text UIName;
    public Text UIStock;
    public Text UIProperty;

    private string _name;
    private float _stock;
    private string _property;
    private float lastTime;

    private void Update()
    {
        if( Time.time - lastTime > 1.0f )
        {
            lastTime = Time.time;
            Stock -= 1.0f;
        }
    }

    public float MaxStock
    {
        set
        {
            Fill.maxValue = value;
        }
    }

    public float Stock
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
    }
}
