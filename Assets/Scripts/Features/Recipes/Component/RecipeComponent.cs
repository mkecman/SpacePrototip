using UnityEngine;
using UnityEngine.Events;
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
    public Button ConvertButton;

    private RecipeModel _model;
    private ColorBlock _buttonColors;

    void Start()
    {
        _buttonColors = ColorBlock.defaultColorBlock;
    }

    void OnEnable()
    {
        Messenger.Listen(AtomMessage.ATOM_STOCK_UPDATED, handleAtomStockUpdated);
    }

    void OnDisable()
    {
        Messenger.StopListening(AtomMessage.ATOM_STOCK_UPDATED, handleAtomStockUpdated);
    }

    public void Setup( RecipeModel model )
    {
        _model = model;
        MolecularWeightLabel.text = _model.MolecularMass.ToString();
        ExchangeRateLabel.text = _model.ExchangeRate.ToString();
        HardCurrencyLabel.text = "0";
        AmountSlider.onValueChanged.AddListener( new UnityAction<float>( UpdateSliderLabel ) );
        ConvertButton.onClick.AddListener(new UnityAction(handleConvertButtonClick));

        for( int i = 0; i < _model.FormulaAtomsList.Count; i++ )
        {
            GameObject go = Instantiate( FormulaAtomPrefab, RecipeFormula );
            FormulaAtomComponent formulaAtom = go.GetComponent<FormulaAtomComponent>();
            formulaAtom.Setup( _model.FormulaAtomsList[ i ] );
            handleAtomStockUpdated(new AtomMessage(_model.FormulaAtomsList[i].AtomicNumber, 0));
        }

        UpdateSliderLabel( 0 );
    }

    private void handleConvertButtonClick()
    {
        Messenger.Dispatch(RecipeMessage.CRAFT_COMPOUND_REQUEST, new RecipeMessage( _model, AmountSlider.value ));
    }

    private void UpdateSliderLabel( float value )
    {
        SliderLabel.text = value.ToString();
        int HCAmount = (int)( _model.MolecularMass * value * _model.ExchangeRate );
        HardCurrencyLabel.text = HCAmount.ToString();
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
            _buttonColors.normalColor = new Color(0,1,0);
            AmountSlider.maxValue = calculateMaxSliderValue();
            AmountSlider.value = AmountSlider.maxValue;
        }
        else
        {
            _buttonColors.normalColor = Color.white;
            AmountSlider.maxValue = 0;
        }
        ConvertButton.colors = _buttonColors;
    }

    private int calculateMaxSliderValue()
    {
        int minValue = int.MaxValue;
        int currentAmount = 0;
        
        for( int i = 0; i < _model.FormulaAtomsList.Count; i++ )
        {
            currentAmount = gameModel.User.Atoms[_model.FormulaAtomsList[i].AtomicNumber].Stock / _model.FormulaAtomsList[i].Amount;
            if ( currentAmount < minValue )
                minValue = currentAmount;
        }
        
        return minValue;
    }
}
