using UniRx;
using UnityEngine;
using UnityEngine.UI;

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
    private bool _isSetup = false;

    public bool AmountSliderActive;

    public void Setup( RecipeModel model )
    {
        _model = model;
        MolecularWeightLabel.text = _model.MolecularMass.ToString();
        ExchangeRateLabel.text = _model.ExchangeRate.ToString();
        HardCurrencyLabel.text = "0";

        AmountSlider.OnValueChangedAsObservable().Subscribe( UpdateSliderLabel ).AddTo( this );
        ConvertButton.OnClickAsObservable().Subscribe( _ => handleConvertButtonClick() ).AddTo( this );

        for( int i = 0; i < _model.FormulaAtomsList.Count; i++ )
        {
            GameObject go = Instantiate( FormulaAtomPrefab, RecipeFormula );
            FormulaAtomComponent formulaAtom = go.GetComponent<FormulaAtomComponent>();
            FormulaAtomModel formulaAtomModel = _model.FormulaAtomsList[ i ];

            if( formulaAtomModel.AtomicNumber < gameModel.User.Atoms.Count )
                listenForAtomStockChange( formulaAtomModel.AtomicNumber );

            formulaAtom.Setup( formulaAtomModel );
        }

        _isSetup = true;

        gameModel.User.Atoms.ObserveAdd().Subscribe( addEvent => listenForAtomStockChange( addEvent.Value.AtomicNumber ) );

        UpdateSliderLabel( 0 );
    }

    private void listenForAtomStockChange( int atomicNumber )
    {
        gameModel.User.Atoms[ atomicNumber ].rStock
            .Where( _ => isActiveAndEnabled && _isSetup )
            .Subscribe( checkAmount )
            .AddTo( this );
    }

    private void checkAmount( int stock )
    {
        if( _model.FormulaAtomsList.TrueForAll( formulaAtom => formulaAtom.HaveEnough.Value ) )
        {
            ConvertButton.interactable = true;
            AmountSlider.maxValue = calculateMaxSliderValue();
            if( !AmountSliderActive )
                AmountSlider.value = AmountSlider.maxValue;
        }
        else
        {
            ConvertButton.interactable = false;
            AmountSlider.maxValue = 0;
        }
    }

    private void handleConvertButtonClick()
    {
        Messenger.Dispatch( RecipeMessage.CRAFT_COMPOUND_REQUEST, new RecipeMessage( _model, (int)AmountSlider.value ) );
    }

    private void UpdateSliderLabel( float value )
    {
        if( value < AmountSlider.maxValue )
            AmountSliderActive = true;

        SliderLabel.text = value.ToString();
        int HCAmount = (int)( _model.MolecularMass * value * _model.ExchangeRate );
        HardCurrencyLabel.text = HCAmount.ToString();
    }

    private int calculateMaxSliderValue()
    {
        int minValue = int.MaxValue;
        int currentAmount = 0;

        for( int i = 0; i < _model.FormulaAtomsList.Count; i++ )
        {
            currentAmount = gameModel.User.Atoms[ _model.FormulaAtomsList[ i ].AtomicNumber ].Stock / _model.FormulaAtomsList[ i ].Amount;
            if( currentAmount < minValue )
                minValue = currentAmount;
        }

        return minValue;
    }

    private void OnEnable()
    {
        AmountSliderActive = false;
        if( _model != null )
            checkAmount( 0 );
    }
}
