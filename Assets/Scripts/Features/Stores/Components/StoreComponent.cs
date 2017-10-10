using UnityEngine;
using UnityEngine.UI;

public class StoreComponent : MonoBehaviour
{
    public ProgressBar Fill;
    public Text UIName;
    public Text UIStock;
    public Text UIMaxStock;
    public Text UIProperty;

    public string ID;
    private string _name;
    private string _property;
    private int _stock;

    public int MaxStock
    {
        set
        {
            Fill.maxValue = value;
            UIMaxStock.text = value.ToString();
        }
    }

    public int Stock
    {
        set
        {
            _stock = value;
            Fill.value = value;
            UIStock.text = value.ToString();
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

}
