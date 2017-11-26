using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UniRx;

public class LifeResistance : AbstractController
{
    public ResistanceChart chart;
    public Text Name;
    public Button increaseMinButton;
    public Button increaseMaxButton;

    public void Setup( string environment, YPlanetModel planet, LifeModel life )
    {
        float factor = 1 / ( planet.environments[ environment ].Model.MaxValue - planet.environments[ environment ].Model.MedValue );
        float resistancePositive = life.resistances[ environment ].Max * factor;

        factor = 1 / ( planet.environments[ environment ].Model.MedValue - planet.environments[ environment ].Model.MinValue );
        float resistanceNegative = life.resistances[ environment ].Min * factor;
        
        Name.text = resistanceNegative + " :"+ gameModel.PlanetaryEnvironments[ environment ].Name + " Resistance: " + resistancePositive;

        //increaseMaxButton.GetComponentInChildren<Text>().text = 
        //increaseMaxButton.OnClickAsObservable().Subscribe( _ => increaseMax() );

        ResistanceChartModel model = new ResistanceChartModel();
        model.UniversalMin = planet.environments[ environment ].Model.MinValue;
        model.UniversalMax = planet.environments[ environment ].Model.MaxValue;
        model.UniversalMed = planet.environments[ environment ].Model.MedValue;
        model.PlanetPoint = planet.environments[ environment ].Model.CurrentValue;
        model.LifeMin = life.resistances[ environment ].Min;
        model.LifeMax = life.resistances[ environment ].Max;
        chart.Setup( model );
    }

    internal void UpdateChartPlanetPoint( float value )
    {
        chart.UpdatePlanetPoint( value );
    }

    private void increaseMax()
    {

    }
}
