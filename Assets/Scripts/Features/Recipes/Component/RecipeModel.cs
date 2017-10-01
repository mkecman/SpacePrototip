using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

[Serializable]
public class RecipeModel
{
    public string Name;
    public string Formula;
    public float MolecularMass;
    public float ExchangeRate;
    public List<FormulaAtomModel> FormulaAtomsList;

    public void Setup( AtomModel[] Atoms )
    {
        string[] splitFormula = Regex.Split( Formula, "([A-Z][a-z])|/d+|([A-Z])" );

        FormulaAtomsList = new List<FormulaAtomModel>();
        for( int i = 0; i < splitFormula.Length; i++ )
        {
            if( splitFormula[ i ] != "" )
            {
                if( splitFormula[ i + 1 ] == "" )
                    splitFormula[ i + 1 ] = "1";

                AtomModel atom = new AtomModel();
                for( int j = 1; j < Atoms.Length; j++ )
                {
                    if( Atoms[ j ].Symbol == splitFormula[ i ] )
                    {
                        atom = Atoms[ j ];
                        break;
                    }
                }

                FormulaAtomsList.Add( new FormulaAtomModel( atom.AtomicNumber, splitFormula[ i ], int.Parse( splitFormula[ i + 1 ] ) ) );
                i++;
            }
        }
        
    }
}
