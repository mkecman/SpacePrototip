using System;
using UniRx;

[Serializable]
public class FormulaAtomModel
{
    public string Symbol;
    public int Amount;
    public int AtomicNumber;
    public BoolReactiveProperty HaveEnough = new BoolReactiveProperty( false );

    public FormulaAtomModel( int atomicNumber, string symbol, int amount )
    {
        AtomicNumber = atomicNumber;
        Symbol = symbol;
        Amount = amount;
    }
}
