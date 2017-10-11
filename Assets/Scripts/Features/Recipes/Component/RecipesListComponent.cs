using UnityEngine;
using System.Collections;
using System;

public class RecipesListComponent : AbstractView
{
    public GameObject recipePrefab;
    private bool _started;

    void OnEnable()
    {
        if( !_started )
        {
            handleGameModelLoaded( null );
        }
    }
    
    // Use this for initialization
    void Start()
    {
        Messenger.Listen(GameMessage.MODEL_LOADED, handleGameModelLoaded);
    }

    private void handleGameModelLoaded(AbstractMessage msg)
    {
        for (int i = 0; i < gameModel.Recipes.Count; i++)
        {
            GameObject go = Instantiate(recipePrefab, gameObject.transform);
            RecipeComponent rc = go.GetComponent<RecipeComponent>();
            rc.Setup(gameModel.Recipes[i]);
        }
        _started = true;
    }
}
