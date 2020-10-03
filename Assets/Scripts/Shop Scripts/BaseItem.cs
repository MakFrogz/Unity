using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseItem : MonoBehaviour
{
    [SerializeField] protected GameObject _keyText;
    [SerializeField] protected int _itemPrice;
    
    protected bool _canBuy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SetKeyActiveText(true);
            _canBuy = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SetKeyActiveText(false);
            _canBuy = false;
        }
    }

    private void SetKeyActiveText(bool val)
    {
        _keyText.SetActive(val);
    }

    protected void DisableItem()
    {
        gameObject.SetActive(false);
    }
}
