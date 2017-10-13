using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class SCManager : AbstractController
{
    public Text SCHUDLabel;
    public AtomSliderComponent SCSlider;
    public Button CreateSolarButton;

    void Start()
    {
        Messenger.Listen( GameMessage.MODEL_LOADED, handleGameModelLoaded );
    }

    public void handleGameModelLoaded( AbstractMessage message )
    {
        gameModel.User.rSC
            .Subscribe( sc => SCHUDLabel.text = "AM:" + (int)Mathf.Floor( sc ) ).AddTo(this);

        gameModel.User.rSC
            .Where( SC => SC > SCSlider.maxValue )
            .Subscribe( SC =>
            {
                if( gameModel.User.SC <= gameModel.Config.maxSC )
                    SCSlider.maxValue = Mathf.Floor( gameModel.User.SC );
                else
                    SCSlider.maxValue = gameModel.Config.maxSC;
            } ).AddTo(this);

        SCSlider.SCSlider.OnValueChangedAsObservable()
            .Subscribe( value => CreateSolarButton.interactable = value >= SCSlider.SCSlider.value )
            .AddTo( this );

        SCSlider.Setup( gameModel.User.SC );
    }

    public void CreateSolar()
    {
        Messenger.Dispatch( SolarMessage.CREATE_SOLAR, new SolarMessage( SCSlider.SCSlider.value, SCSlider.maxValue ) );
    }

}
