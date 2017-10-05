using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class FormulaAtomComponent : AbstractView
{
    private FormulaAtomModel _model;
    public Text Symbol;
    public Text Amount;

    public void Setup( FormulaAtomModel model )
    {
        _model = model;
        Symbol.text = _model.Symbol;
        Amount.text = _model.Amount.ToString();
        handleAtomStockUpdated(new AtomMessage(_model.AtomicNumber, 0));
    }
    
    void OnEnable()
    {
        Messenger.Listen( AtomMessage.ATOM_STOCK_UPDATED, handleAtomStockUpdated );
    }

    void OnDisable()
    {
        Messenger.StopListening(AtomMessage.ATOM_STOCK_UPDATED, handleAtomStockUpdated);
    }

    private void handleAtomStockUpdated( AbstractMessage message )
    {
        if (_model.AtomicNumber >= gameModel.User.Atoms.Count)
        {
            setHaveEnough( false );
            return;
        }
        
        AtomModel atom = gameModel.User.Atoms[ _model.AtomicNumber ];
        if( atom.Stock >= _model.Amount )
        {
            setHaveEnough( true );
        }
        else
        {
            setHaveEnough( false );
        }
    }

    private void setHaveEnough( bool itHas )
    {
        if( itHas )
        {
            _model.HaveEnough = true;
            Symbol.color = gameModel.Config.GreenColor;
            Amount.color = gameModel.Config.GreenColor;
        }
        else
        {
            _model.HaveEnough = false;
            Symbol.color = Color.red;
            Amount.color = Color.red;
        }
    }
    
}
