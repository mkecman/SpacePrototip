﻿using UnityEngine;
using System.Collections;
using System;

public class RecipesManager : AbstractController
{

    // Use this for initialization
    void Start()
    {
        Messenger.Listen(RecipeMessage.CRAFT_COMPOUND_REQUEST, handleCraftCompoundRequest);
    }

    private void handleCraftCompoundRequest(AbstractMessage msg)
    {
        RecipeMessage recipeMessage = msg as RecipeMessage;
        RecipeModel model = recipeMessage.Model;

        for (int i = 0; i < recipeMessage.Amount; i++)
        {
            for (int j = 0; j < model.FormulaAtomsList.Count; j++)
            {
                FormulaAtomModel atom = model.FormulaAtomsList[j];
                gameModel.User.Atoms[atom.AtomicNumber].Stock -= atom.Amount;
                Messenger.Dispatch(AtomMessage.ATOM_STOCK_UPDATED, new AtomMessage(atom.AtomicNumber, -atom.Amount));
            }
        }

        Messenger.Dispatch(HCMessage.UPDATED, new HCMessage(Mathf.RoundToInt(model.MolecularMass * model.ExchangeRate * recipeMessage.Amount)));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
