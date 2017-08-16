using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HarvesterComponent : AbstractView
{
    public Text UIHarvestRate;
    public Slider UIUpgradeLevelSlider;
    private int _harvestRate;
    private float _upgradeLevel;

    public int HarvestRate
    {
        set
        {
            _harvestRate = value;
            UIHarvestRate.text = _harvestRate + "";
        }
        get
        {
            return _harvestRate;
        }
    }

    public float UpgradeLevel
    {
        set
        {
            _upgradeLevel = value;
            UIUpgradeLevelSlider.value = _upgradeLevel;
        }
    }
}
