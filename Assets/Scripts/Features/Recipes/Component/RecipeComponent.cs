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

        UpdateSliderLabel( 0 );
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
        Messenger.Listen( AtomMessage.ATOM_STOCK_UPDATED, handleAtomStockUpdated );
    }

    private void handleAtomStockUpdated( AbstractMessage message )
    {
        AtomMessage msg = message as AtomMessage;
        bool isEnough = true;
        for( int i = 0; i < _model.FormulaAtomsList.Count; i++ )
        {
            if( !_model.FormulaAtomsList[ i ].HaveEnough )
            {
                isEnough = false;
                break;
            }
        }
        if( isEnough )
        {
            AmountSlider.maxValue = calculateMaxSliderValue();
        }
        else
        {
            AmountSlider.maxValue = 0;
            
        }

    }

    private int calculateMaxSliderValue()
    {
        int maxValue = 0;
        int maxAmountAtomicNumber = 0;
        int maxAmount = 0;


        for( int i = 0; i < _model.FormulaAtomsList.Count; i++ )
        {
            if( maxAmount <= _model.FormulaAtomsList[ i ].Amount )
            {
                int candidateAN = _model.FormulaAtomsList[ i ].AtomicNumber;
                if( maxAmountAtomicNumber == 0 )
                    maxAmountAtomicNumber = candidateAN;

                if( gameModel.User.Atoms[ candidateAN ].Stock <= gameModel.User.Atoms[ maxAmountAtomicNumber ].Stock )
                {
                    maxAmount = _model.FormulaAtomsList[ i ].Amount;
                    maxAmountAtomicNumber = _model.FormulaAtomsList[ i ].AtomicNumber;
                }
            }
        }

        int atomStock = gameModel.User.Atoms[ maxAmountAtomicNumber ].Stock;
        maxValue = atomStock / maxAmount;

        return maxValue;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
