using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ResistanceChart : MonoBehaviour
{
    public ResistanceChartRange lifeRange;
    public ResistanceChartPoint medPoint;
    public ResistanceChartPoint planetPoint;
    public Text minLabel;
    public Text maxLabel;

    private ResistanceChartModel model;
    private float factor;

    public void Setup( ResistanceChartModel model )
    {
        this.model = model;

        minLabel.text = model.UniversalMin.ToString();
        maxLabel.text = model.UniversalMax.ToString();
        medPoint.Setup( model.UniversalMed );
        lifeRange.Setup( model.LifeMin, model.LifeMax );

        RectTransform lifeRect = lifeRange.GetComponent<RectTransform>();
        RectTransform planetRect = planetPoint.GetComponent<RectTransform>();
        RectTransform medRect = medPoint.GetComponent<RectTransform>();
        RectTransform chart = gameObject.transform as RectTransform;

        factor = chart.rect.width / ( model.UniversalMax - model.UniversalMin );
        
        lifeRect.SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, (model.LifeMax - model.LifeMin) * factor );
        
        lifeRect.anchoredPosition = new Vector2( ( model.LifeMin - model.UniversalMin ) * factor, lifeRect.anchoredPosition.y );
        medRect.anchoredPosition = new Vector2( (model.UniversalMed - model.UniversalMin) * factor, medRect.anchoredPosition.y );

        UpdatePlanetPointPosition();
    }

    internal void UpdatePlanetPoint( float value )
    {
        if( model != null )
        {
            model.PlanetPoint = value;
            UpdatePlanetPointPosition();
        }
    }

    private void UpdatePlanetPointPosition()
    {
        RectTransform planetRect = planetPoint.GetComponent<RectTransform>();
        float planetRange = model.PlanetPoint - model.UniversalMin;
        planetRect.anchoredPosition = new Vector2( planetRange * factor, planetRect.anchoredPosition.y );
        planetPoint.Setup( model.PlanetPoint );
    }
}
