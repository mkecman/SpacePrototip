﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

public class AtomSliderComponent : AbstractView
{
    public GameObject AtomSliderLabelPrefab;
    public RectTransform AtomsLabelContainer;
    public Slider SCSlider;
    public Text SCValueLabel;
    private Dictionary<int, RectTransform> atomLabelRects;

    void Start()
    {
        atomLabelRects = new Dictionary<int, RectTransform>();
        SCSlider.onValueChanged.AddListener( handleSCSliderValueChanged );
    }

    public void Setup( float SC )
    {
        SCSlider.minValue = gameModel.Config.minSC + 2;
        setMaxSliderValue( SC );
    }

    private void setMaxSliderValue( float value )
    {
        SCSlider.maxValue = value;

        float maxWidth = AtomsLabelContainer.rect.size.x;
        Dictionary<int, float> atomPositions = new Dictionary<int, float>();
        float factor = value - gameModel.Config.minSC;

        foreach( KeyValuePair<int, int> range in gameModel.Config.atomSCUnlockRanges )
        {
            if( range.Value <= value )
                atomPositions.Add( range.Key, ( ( ( range.Value - gameModel.Config.minSC ) / factor ) * maxWidth ) - maxWidth / 2 );
            else
                break;
        }

        
        for( int i = 1; i <= atomPositions.Count; i++ )
        {
            if( i > atomLabelRects.Count )
            {
                GameObject go = Instantiate( AtomSliderLabelPrefab, AtomsLabelContainer );
                Text label = go.GetComponent<Text>();
                label.text = gameModel.Atoms[ i ].Symbol;

                RectTransform rect = go.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector3( atomPositions[ i ], rect.anchoredPosition.y, 0 );

                atomLabelRects.Add( i, rect );
            }
            
            atomLabelRects[i].anchoredPosition = new Vector3( atomPositions[ i ], atomLabelRects[ i ].anchoredPosition.y, 0 );
        }

        handleSCSliderValueChanged( SCSlider.value );
    }
    
    private void handleSCSliderValueChanged( float value )
    {
        SCValueLabel.text = value.ToString();
        
        int currentAtomicNumber = getMaxAtomicNumberFromSC( value );
        int maxAtomicNumber = atomLabelRects.Count;
        RectTransform rect;
        float scale;
        float atomWeight;
        
        for( int i = 1; i <= maxAtomicNumber; i++ )
        {
            atomWeight = (1 / Mathf.Sqrt(2 * Mathf.PI * gameModel.Config.curve)) * Mathf.Exp(-Mathf.Pow(currentAtomicNumber - i, 2) / (2 * gameModel.Config.curve));
            rect = atomLabelRects[i];
            scale = 1f - (1f - (4f * atomWeight));
            rect.localScale = new Vector3(scale, scale);
            rect.anchoredPosition = new Vector3(rect.anchoredPosition.x, 20f * scale, 0);
        }
    }

    int getMaxAtomicNumberFromSC( float SC )
    {
        int result = 1;
        foreach( KeyValuePair<int, int> range in gameModel.Config.atomSCUnlockRanges )
        {
            if( SC >= range.Value )
            {
                result = range.Key;
            }
            else
                break;
        }

        return result;
    }

    public float maxValue
    {
        get { return SCSlider.maxValue; }
        set { setMaxSliderValue( value ); }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
