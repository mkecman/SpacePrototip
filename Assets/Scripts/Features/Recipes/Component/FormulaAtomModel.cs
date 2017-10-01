using System;

[Serializable]
public class FormulaAtomModel
{
    public string Symbol;
    public int Amount;

    public FormulaAtomModel( string symbol, int amount )
    {
        Symbol = symbol;
        Amount = amount;
    }
}
