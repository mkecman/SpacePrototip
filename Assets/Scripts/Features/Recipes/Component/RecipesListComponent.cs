using UnityEngine;
using System.Collections;
using System;

public class RecipesListComponent : AbstractView
{
    public GameObject recipePrefab;
    
    void OnEnable()
    {
        
    }
    
    // Use this for initialization
    void Start()
    {
        for( int i = 0; i < gameModel.Recipes.Count; i++ )
        {
            GameObject go = Instantiate( recipePrefab, gameObject.transform );
            RecipeComponent rc = go.GetComponent<RecipeComponent>();
            rc.Setup( gameModel.Recipes[ i ] );
        }
    }
    
    // Update is called once per frame
    void Update()
    {

    }
}
