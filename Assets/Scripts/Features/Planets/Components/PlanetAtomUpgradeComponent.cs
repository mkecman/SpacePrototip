using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlanetAtomUpgradeComponent : MonoBehaviour
{
    public Text UIStockLabel;
    public Text UIPriceLabel;
    public Button UIUpgradeButton;
    
    private PlanetAtomModel _model;

    void Start()
    {
        UIUpgradeButton.onClick.AddListener( new UnityAction( DispatchUpgradeMessage ) );
    }

    public void Setup( PlanetAtomModel model )
    {
        _model = model;

        UIStockLabel.text = model.HarvestRate.ToString();
        UIPriceLabel.text = "bla";
    }

    private void DispatchUpgradeMessage()
    {
        //Messenger.Dispatch( AtomMessage.ATOM_STOCK_UPGRADE, new AtomMessage( _atomicNumber, 1 ) );
    }
}
