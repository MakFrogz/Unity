using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreHealthItem : BaseItem
{

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && _canBuy)
        {
            if (LevelManager.instance.TrySpendCoin(_itemPrice))
            {
                ItemEffect();
                AudioManager.instance.PlaySFX(18);
                DisableItem();
            }
            else
            {
                AudioManager.instance.PlaySFX(19);
            }
        }
    }

    private void ItemEffect()
    {
            PlayerHealthController.instance.RestoreHealth();
    }
}
