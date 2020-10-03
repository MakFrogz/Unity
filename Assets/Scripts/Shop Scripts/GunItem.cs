using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunItem : BaseItem
{
    [SerializeField] private Gun[] _guns;
    private SpriteRenderer _spriteRenderer;
    private Gun _shopGun;
    void Start()
    {
        int gunIndex = Random.Range(0, _guns.Length);
        _shopGun = _guns[gunIndex];
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _shopGun.GetGunShopUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _canBuy)
        {
            if (LevelManager.instance.TrySpendCoin(_itemPrice))
            {
                AddGun();
                AudioManager.instance.PlaySFX(18);
                DisableItem();
            }
            else
            {
                AudioManager.instance.PlaySFX(19);
            }
        }
    }

    private void AddGun()
    {
        bool hasGun = false;
        List<Gun> guns = PlayerController.instance.GetPlayersGuns();
        foreach (Gun gun in guns)
        {
            if (_shopGun.GetGunName() == gun.GetGunName())
            {
                hasGun = true;
            }
        }

        if (!hasGun)
        {
            Gun gunClone = Instantiate(_shopGun);
            gunClone.transform.parent = PlayerController.instance.GetParentWeapon();
            gunClone.transform.localPosition = Vector3.zero;
            gunClone.transform.localScale = Vector3.one;
            gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);

            PlayerController.instance.AddGun(gunClone);
            PlayerController.instance.SetCurrentWaepon(guns.Count - 1);
            PlayerController.instance.SwitchWeapon();
        }
    }
}
