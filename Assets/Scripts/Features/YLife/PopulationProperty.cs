using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UniRx;

public class PopulationProperty : MonoBehaviour
{
    public Text ValueLabel;
    public Slider ValueSlider;

    public void Setup( FloatReactiveProperty property )
    {
        ValueSlider.value = property.Value;
        ValueSlider.OnValueChangedAsObservable().Subscribe( _ => property.Value = _ );
        property.Subscribe( _ => ValueLabel.text = _.ToString() );
    }
}
