using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

class MenuEditor
{
    [MenuItem( "Space/Play-Stop _%0" )]
    public static void PlayFromPrelaunchScene()
    {
        if( EditorApplication.isPlaying == true )
        {
            EditorApplication.isPlaying = false;
            //SceneManager.LoadScene( File.ReadAllText( ".lastScene" ) );
            //EditorSceneManager.OpenScene( File.ReadAllText( ".lastScene" ) );
            EditorApplication.OpenScene( File.ReadAllText( ".lastScene" ) );
            return;
        }

        File.WriteAllText( ".lastScene", EditorSceneManager.GetActiveScene().name );

        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene( "Assets/Scenes/Main.unity" );
        EditorApplication.isPlaying = true;
    }
}
