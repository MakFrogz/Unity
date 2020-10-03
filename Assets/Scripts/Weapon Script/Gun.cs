using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireRate = 2f;
    [SerializeField] private Sprite _gunUI;
    [SerializeField] private Sprite _gunShopUI;
    [SerializeField] private string _gunName;

    private bool _isShoot = false;
    private float _nextFireTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time >= _nextFireTime)
            {
                _isShoot = true;
                _nextFireTime = Time.time + 1f / _fireRate;
            }
        }
    }

    private void FixedUpdate()
    {
        Shoot();
    }
    private void Shoot()
    {
        if (_isShoot)
        {
            AudioManager.instance.PlaySFX(12);
            Instantiate(_bullet, _firePoint.position, _firePoint.rotation);
        }
        _isShoot = false;
    }

    public Sprite GetGunUI()
    {
        return _gunUI;
    }

    public Sprite GetGunShopUI()
    {
        return _gunShopUI;
    }

    public string GetGunName()
    {
        return _gunName;
    }
}
