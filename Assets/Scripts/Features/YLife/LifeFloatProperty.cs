using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UniRx;

public class LifeFloatProperty : MonoBehaviour
{
    public Text Label;
    public Text Value;

    public void Setup( string label, FloatReactiveProperty value )
    {
        Label.text = label;
        value.Subscribe( _ => Value.text = _.ToString() ).AddTo(this);
    }
}
