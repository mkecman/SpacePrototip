using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public GameObject Fill;
    
    private float _value = 0f;
    private float _maxValue = 100f;

    private RectTransform _fillRect;
    private Image _fillImage;
    private float _width;

    void Awake()
    {
        _fillRect = Fill.GetComponent<RectTransform>();
        _fillImage = Fill.GetComponent<Image>();
    }

    private void Start()
    {
        _width = gameObject.GetComponent<RectTransform>().rect.width;
        setFill();
    }

    public float value
    {
        set { _value = value; setFill(); }
        get { return _value; }
    }

    public float maxValue
    {
        set { _maxValue = value; setFill(); }
        get { return _maxValue; }
    }

    private void setFill()
    {
        _fillRect.sizeDelta = new Vector2( ( _value / _maxValue ) * _width, _fillRect.sizeDelta.y );
        
        if( _value == _maxValue )
            _fillImage.color = Color.red;
        else
            _fillImage.color = Color.white;
    }

    private void OnDestroy()
    {
        _fillImage = null;
        _fillImage = null;
        Fill = null;
    }
}
