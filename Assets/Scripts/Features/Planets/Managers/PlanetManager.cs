using UnityEngine;
using System.Collections;

public class PlanetManager : MonoBehaviour
{
    public GameObject planetPrefab;

    // Use this for initialization
    void Start()
    {
        Messenger.Listen( PlanetMessage.CREATE_PLANET, handleCreatePlanet );
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void handleCreatePlanet( AbstractMessage message )
    {
        PlanetMessage msg = message as PlanetMessage;
        for( int index = 0; index < msg.Planets.Count; index++ )
        {
            GameObject planet = Instantiate( planetPrefab, msg.Container );
            PlanetComponent planetView = planet.GetComponent<PlanetComponent>();
            planetView.Setup( msg.Planets[ index ] );
        }
    }

    
}
