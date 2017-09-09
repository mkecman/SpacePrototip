using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class HarvesterComponent : AbstractView
{
    public Text UIHarvestRate;
    public Slider UIUpgradeLevelSlider;
    public Button UIUpgradeButton;

    private PlanetAtomModel _model;

    void Start()
    {
        UIUpgradeButton.onClick.AddListener( new UnityEngine.Events.UnityAction( Upgrade ) );
    }

    public void Upgrade()
    {
        if( gameModel.User.XP - 1 >= 0 )
        {
            gameModel.User.XP -= 1;
            _model.HarvestRate += 1;
            _model.UpgradeLevel += 1;
            Messenger.Dispatch( HarversterMessage.HARVESTER_UPGRADED );
            updateView();
        }
        else
        {
            Debug.Log( "Not enough XP to upgrade! Create more Solars!" );
        }
        
    }

    internal void Setup( PlanetAtomModel value )
    {
        _model = value;
        updateView();
    }

    private void updateView()
    {
        UIHarvestRate.text = _model.HarvestRate + "";
        UIUpgradeLevelSlider.value = _model.UpgradeLevel;
    }
    
}
