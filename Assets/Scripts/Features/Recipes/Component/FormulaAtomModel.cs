using System;

[Serializable]
public class FormulaAtomModel
{
    public string Symbol;
    public int Amount;
    public int AtomicNumber;
    public bool HaveEnough = false;

    public FormulaAtomModel( int atomicNumber, string symbol, int amount )
    {
        AtomicNumber = atomicNumber;
        Symbol = symbol;
        Amount = amount;
    }
}
