using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Globalization;

public class PlanetProperty : MonoBehaviour
{
    public PlanetaryEnvironmentModel Model;

    public Text PropertyLabel;
    public Slider ValueSlider;
    public Text ValueSliderLabel;
    public Text AttackFactorLabel;
    
    public void Setup( PlanetaryEnvironmentModel model )
    {
        Model = model;
        Model.rStrength.Subscribe( _ => AttackFactorLabel.text = _.ToString() );
        Model.rCurrentValue.Subscribe( _ => ValueSliderLabel.text = _.ToString() );
        PropertyLabel.text = Model.Name;
        ValueSlider.minValue = Model.MinValue;
        ValueSlider.maxValue = Model.MaxValue;
        ValueSlider.value = Model.CurrentValue;
        ValueSlider.OnValueChangedAsObservable().Subscribe( _ => Model.CurrentValue = _ ).AddTo( this );
    }
    
}
