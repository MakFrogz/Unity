using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickUp : MonoBehaviour
{
    [SerializeField] private float _waitToBeCollected = 0.5f;
    [SerializeField] private Gun _gun;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_waitToBeCollected > 0)
        {
            _waitToBeCollected -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && _waitToBeCollected <= 0)
        {
            bool hasGun = false;
            List<Gun> guns = PlayerController.instance.GetPlayersGuns();
            foreach (Gun gun in guns)
            {
                if(_gun.GetGunName() == gun.GetGunName())
                {
                    hasGun = true;
                }
            }

            if (!hasGun)
            {
                Gun gunClone = Instantiate(_gun);
                gunClone.transform.parent = PlayerController.instance.GetParentWeapon();
                gunClone.transform.localPosition = Vector3.zero;
                gunClone.transform.localScale = Vector3.one;
                gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);

                PlayerController.instance.AddGun(gunClone);
                PlayerController.instance.SetCurrentWaepon(guns.Count - 1);
                PlayerController.instance.SwitchWeapon();
            }
            Destroy(gameObject);
        }
    }
}
