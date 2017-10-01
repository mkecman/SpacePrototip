using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class RecipeComponent : AbstractView
{
    public GameObject FormulaAtomPrefab;
    public Transform RecipeFormula;
    public Text MolecularWeightLabel;
    public Text ExchangeRateLabel;
    public Text HardCurrencyLabel;
    public Slider AmountSlider;
    public Text SliderLabel;

    private RecipeModel _model;

    public void Setup( RecipeModel model )
    {
        _model = model;
        MolecularWeightLabel.text = _model.MolecularMass.ToString();
        ExchangeRateLabel.text = _model.ExchangeRate.ToString();
        HardCurrencyLabel.text = "0";
        AmountSlider.onValueChanged.AddListener( new UnityEngine.Events.UnityAction<float>( UpdateSliderLabel ) );

        for( int i = 0; i < _model.FormulaAtomsList.Count; i++ )
        {
            GameObject go = Instantiate( FormulaAtomPrefab, RecipeFormula );
            FormulaAtomComponent formulaAtom = go.GetComponent<FormulaAtomComponent>();
            formulaAtom.Setup( _model.FormulaAtomsList[ i ] );
        }
    }

    private void UpdateSliderLabel( float value )
    {
        SliderLabel.text = value.ToString();
        int HCAmount = (int)( _model.MolecularMass * value * _model.ExchangeRate );
        HardCurrencyLabel.text = HCAmount.ToString();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
