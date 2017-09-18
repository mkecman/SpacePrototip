using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class AtomStockUpgradeComponent : MonoBehaviour
{
    public Text UIStockLabel;
    public Text UISCLabel;
    private int _atomicNumber = 0;

    void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener( new UnityAction( DispatchUpgradeMessage ) );
    }

    public void Setup( int atomicNumber, int stock, int SC )
    {
        _atomicNumber = atomicNumber;
        UIStockLabel.text = stock.ToString();
        UISCLabel.text = SC.ToString();
    }

    private void DispatchUpgradeMessage()
    {
        Messenger.Dispatch( AtomMessage.ATOM_STOCK_UPGRADE, new AtomMessage( _atomicNumber, 1, gameObject ) );
    }
}
