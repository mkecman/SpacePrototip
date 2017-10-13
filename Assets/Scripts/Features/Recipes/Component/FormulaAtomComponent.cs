using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UniRx;

public class FormulaAtomComponent : AbstractView
{
    public Text Symbol;
    public Text Amount;

    private FormulaAtomModel _model;
    private Color _color;

    public void Setup( FormulaAtomModel model )
    {
        _model = model;
        Symbol.text = _model.Symbol;
        Amount.text = _model.Amount.ToString();

        gameModel.User.Atoms.ObserveAdd().Subscribe( addEvent => listenForAtomStockChange( addEvent.Value.AtomicNumber ) );

        if( _model.AtomicNumber < gameModel.User.Atoms.Count )
            listenForAtomStockChange( _model.AtomicNumber );

        _model.HaveEnough
            .Select(x => x ? _color = gameModel.Config.GreenColor : _color = Color.red)
            .Subscribe(_ => { Symbol.color = _color; Amount.color = _color; } )
            .AddTo(this);
    }

    private void listenForAtomStockChange( int atomicNumber )
    {
        if( _model.AtomicNumber < gameModel.User.Atoms.Count )
            gameModel.User.Atoms[ _model.AtomicNumber ].rStock
                .Where( _ => isActiveAndEnabled )
                .Subscribe( stock => _model.HaveEnough.Value = ( stock >= _model.Amount ) )
                .AddTo( this );
    }

}
