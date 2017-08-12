using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AtomView : MonoBehaviour
{
    public Slider slider;
    public Text UIName;

    public Text UIStock;
    public Text UIProperty;

    public AtomModel model;

    void Awake()
    {
        
    }

    void OnEnable()
    {
        
    }

    public void SetAtom( object _model )
    {
        model = _model as AtomModel;
        UIName.text = model.Symbol;
        UIProperty.text = model.AtomicNumber.ToString();
        UIStock.text = "" + model.Stock;
        slider.maxValue = model.MaxStock;
        slider.value = model.Stock;        
    }


    public float Stock
    {
        get
        {
            return model.Stock;
        }
        
        set
        {
            model.Stock = value;
            slider.value = value;
            UIStock.text = "" + value;
        }
    }
}
