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
    }

    // Use this for initialization
    void Start()
    {
        Messenger.Listen( AtomMessage.ATOM_STOCK_UPDATED, handleAtomStockUpdated );
    }

    private void handleAtomStockUpdated( AbstractMessage message )
    {
        AtomMessage msg = message as AtomMessage;
        AtomModel atom = gameModel.User.Atoms[ msg.AtomicNumber ];
        if( atom.Symbol == _model.Symbol )
        {
            if( atom.Stock >= _model.Amount )
            {
                Symbol.color = Color.green;
                Amount.color = Color.green;
            }
            else
            {
                Symbol.color = Color.red;
                Amount.color = Color.red;
            }
        }
    }
    
}
