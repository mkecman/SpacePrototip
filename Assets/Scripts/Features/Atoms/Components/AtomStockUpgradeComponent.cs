using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class AtomStockUpgradeComponent : AbstractView
{
    public Text UIStockLabel;
    public Text UISCLabel;
    public Button UIButton;

    private int _atomicNumber = 0;
    private int _stock = 0;
    private int _price = 0;

    void Start()
    {
        UIButton = gameObject.GetComponent<Button>();
        UIButton.onClick.AddListener( new UnityAction( DispatchUpgradeMessage ) );

        Messenger.Listen( AtomMessage.ATOM_STOCK_UPDATED, handleAtomStockUpdated );
    }

    private void handleAtomStockUpdated( AbstractMessage message )
    {
        if( gameModel.User.SC < _price )
        {
            UIButton.interactable = false;
        }
        else
        {
            UIButton.interactable = true;
        }
    }

    public void Setup( int atomicNumber, int stock, int price )
    {
        _atomicNumber = atomicNumber;
        _stock = stock;
        _price = price;

        UIStockLabel.text = "+" + stock.ToString();
        UISCLabel.text = price.ToString();
    }

    private void DispatchUpgradeMessage()
    {
        Messenger.Dispatch( AtomMessage.ATOM_STOCK_UPGRADE, new AtomMessage( _atomicNumber, 1 ) );
    }
}
