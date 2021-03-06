﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UniRx;

public class AtomsModelManager
{
    private UserModel _user;
    private ReactiveCollection<AtomModel> _atomsDefinitions;
    private GameConfig _config;

    public void Setup( GameModel gameModel )
    {
        _config = gameModel.Config;
        _atomsDefinitions = gameModel.Atoms;
        _user = gameModel.User;

        UpdateSC();
    }

    private void UpdateSC()
    {
        float collectedSC = 0;
        for( int i = 1; i < _user.Atoms.Count; i++ )
        {
            collectedSC += _user.Atoms[ i ].AtomicWeight * _user.Atoms[ i ].Stock;
        }

        _user.SC = collectedSC;
    }

    public bool GenerateAtom()
    {
        int randomAtomIndex = UnityEngine.Random.Range( 1, _user.Atoms.Count );
        int maxStock = _user.Atoms[ randomAtomIndex ].MaxStock;
        int newStock = _user.Atoms[ randomAtomIndex ].Stock + 1;

        if( newStock <= maxStock )
        {
            UpdateAtomStock( randomAtomIndex, 1 );
            return true;
        }

        return false;
    }

    public void HarvestAtom( int atomicNumber )
    {
        if( atomicNumber >= _user.Atoms.Count )
        {
            AtomModel atom;
            for( int i = _user.Atoms.Count; i <= atomicNumber; i++ )
            {
                atom = _atomsDefinitions[ i ].Copy();
                _user.Atoms.Add( atom );
            }
        }

        if( _user.Atoms[ atomicNumber ].Stock < _user.Atoms[ atomicNumber ].MaxStock )
            UpdateAtomStock( atomicNumber, 1 );
    }

    public void UpdateAtomStock( int atomicNumber, int delta )
    {
        _user.Atoms[ atomicNumber ].Stock += delta;
        _user.SC += _user.Atoms[ atomicNumber ].AtomicWeight * delta;
    }
    
    public bool UpgradeAtomStock( int atomicNumber )
    {
        AtomModel atomModel = _user.Atoms[ atomicNumber ];

        if( !SpendAtoms( atomModel.MaxStockUpgradePrice ) )
        {
            Debug.Log( "NOT ENOUGH ATOMIC MASS TO UPGRADE!" );
            return false;
        }

        atomModel.MaxStock += atomModel.MaxStockNextLevel;
        
        return true;
    }

    public bool SpendAtoms( float PriceSC )
    {
        //if( _config.autoPlay )
         //   return true;

        if( _user.SC < PriceSC )
            return false;

        List<bool> depleted = new List<bool>( _user.Atoms.Count );
        depleted.Add(true);
        for( int index = 1; index < _user.Atoms.Count; index++ )
        {
            depleted.Add( false );
        }

        float remainingSC = PriceSC;
        while( remainingSC > 1f )
        {
            if( depleted.TrueForAll( value => value ) )
            {
                Debug.LogError( "Depleted Atoms, but remainingSC still above 1AM! " + remainingSC );
                remainingSC = 0;
            }
            for( int index = 1; index < _user.Atoms.Count; index++ )
            {
                AtomModel atom = _user.Atoms[ index ];
                if( atom.Stock > 0 )
                {
                    float reducedSC = remainingSC - atom.AtomicWeight;
                    if( reducedSC >= 0 )
                        UpdateAtomStock( atom.AtomicNumber, -1 );

                    remainingSC = reducedSC;
                }
                else
                    depleted[ atom.AtomicNumber ] = true;
            }
        }
        
        return true;
    }
    
}
