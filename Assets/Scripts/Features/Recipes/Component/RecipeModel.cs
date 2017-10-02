using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

[Serializable]
public class RecipeModel
{
    public string Name;
    public string Formula;
    public float MolecularMass;
    public float ExchangeRate;
    public List<FormulaAtomModel> FormulaAtomsList;

    private void splitFormula( AtomModel[] Atoms, string formula, int multiplier = 1 )
    {
        var pattern = @"\((.*?)\)|([A-Z][a-z])|([A-Z])|\d"; //find between brackets, Uppercase+lowercase, Uppercase, digit
        var matches = Regex.Matches( formula, pattern );

        for( int i = 0; i < matches.Count; i++ )
        {
            string symbol = matches[ i ].ToString();
            if( symbol.StartsWith( "(" ) )
            {
                symbol = symbol.Substring( 1, symbol.Length - 1 );
                int result = 1;
                int.TryParse( matches[ i + 1 ].ToString(), out result );
                splitFormula( Atoms, symbol, result );
                i++;
            }
            else
            {
                AtomModel atom = new AtomModel();
                for( int j = 1; j < Atoms.Length; j++ )
                {
                    if( Atoms[ j ].Symbol == symbol )
                    {
                        atom = Atoms[ j ];
                        break;
                    }
                }

                int amount = 1;
                if( i + 1 < matches.Count )
                {
                    int number;
                    var isNumeric = int.TryParse( matches[ i + 1 ].ToString(), out number );
                    if( isNumeric )
                    {
                        amount = number;
                        i++;
                    }
                }
                amount *= multiplier;

                FormulaAtomsList.Add( new FormulaAtomModel( atom.AtomicNumber, symbol, amount ) );
            }
        }
    }

    public void Setup( AtomModel[] Atoms )
    {
        FormulaAtomsList = new List<FormulaAtomModel>();
        splitFormula( Atoms, Formula );
    }
}
