using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody2D _playerBody;
    [SerializeField] private Transform _weaponAim;
    [SerializeField] private Animator _animator;
    /*[SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireRate = 2f;*/
    [SerializeField] private SpriteRenderer _playerSprite;
    [SerializeField] private float _dashCoolDownTime = 2f;
    [SerializeField] private float _dashDuration = 1f;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _invulnerabilityDashTime = 0.8f;
    [SerializeField] private List<Gun> _playerGuns = new List<Gun>();

    private Vector3 _mousePos;
    private Vector3 _moveInput;
    private float _activeSpeed;
    private Camera _camera;
    //private bool _isShoot = false;
    private bool _isDash = false;
    //private float _nextFireTime;
    private float _nextDashTime;
    private float _disableDashTime;
    private bool _canMove = true;
    private int currentWeaponIndex;

    public static PlayerController instance = null;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        /*instance = this;
        DontDestroyOnLoad(this);*/
    }
    void Start()
    {
        _camera = Camera.main;
        //_nextFireTime = 0f;
        _nextDashTime = 0f;
        _activeSpeed = _moveSpeed;
        updateGunUI();

    }

    void Update()
    {
        if (_canMove)
        {
            _moveInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
            _moveInput.Normalize();
            _mousePos = CameraController.instance.GetMainCamera().ScreenToWorldPoint(Input.mousePosition);

            /*if (Input.GetMouseButton(0))
            {
                if (Time.time >= _nextFireTime)
                {
                    _isShoot = true;
                    _nextFireTime = Time.time + 1f / _fireRate;
                }
            }*/

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isDash = true;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                currentWeaponIndex++;
                if (currentWeaponIndex == _playerGuns.Count)
                {
                    currentWeaponIndex = 0;
                }
                SwitchWeapon();
            }
        }
        else
        {
            _moveInput = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        Move();
        Dash();
        WeaponRotation();
        FlipPlayer();
        //Shoot();
    }

    private void Dash()
    {
        if (_isDash && Time.time >= _nextDashTime)
        {
            AudioManager.instance.PlaySFX(8);
            _animator.SetTrigger("Dash");
            PlayerHealthController.instance.setInvulnerabilityTime(_invulnerabilityDashTime);
            _activeSpeed = _moveSpeed + _dashSpeed;
            _disableDashTime = Time.time + _dashDuration;
            _nextDashTime = Time.time + _dashCoolDownTime;
        }

        if(Time.time >= _disableDashTime)
        {
            _activeSpeed = _moveSpeed;
        }
        _isDash = false;
    }

   /* private void Shoot()
    {
        if (_isShoot)
        {
            AudioManager.instance.PlaySFX(12);
            Instantiate(_bullet, _firePoint.position, _firePoint.rotation);
        }
        _isShoot = false;
    }*/

    public void SwitchWeapon()
    {
        foreach (Gun gun in _playerGuns)
        {
            gun.gameObject.SetActive(false);
        }
        _playerGuns[currentWeaponIndex].gameObject.SetActive(true);
        updateGunUI();
    }

    private void Move()
    {
        _playerBody.velocity = _moveInput * _activeSpeed;
        bool playerHasVelocity = _moveInput != Vector3.zero;
        _animator.SetBool("isWalking", playerHasVelocity);
    }

    private void WeaponRotation()
    {
        Vector3 mouseDir = _mousePos - transform.localPosition;
        float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
        _weaponAim.eulerAngles = new Vector3(0f, 0f, angle);
    }

    private void FlipPlayer()
    {
        if(_mousePos.x > _playerBody.position.x)
        {
            transform.localScale = Vector3.one;
            _weaponAim.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            _weaponAim.localScale = new Vector3(-1f, -1f, 1f);
        }
    }

    public Color getPlayerSpriteColor()
    {
        return _playerSprite.color;
    }

    public void setPlayerSpriteColor(Color color)
    {
        _playerSprite.color = color;
    }

    public float getDisableDashTime()
    {
        return _disableDashTime;
    }

    public void setMove(bool value)
    {
        _canMove = value;
    }

    public List<Gun> GetPlayersGuns()
    {
        return _playerGuns;
    }

    public void AddGun(Gun gun)
    {
        _playerGuns.Add(gun);
    }
    public Transform GetParentWeapon()
    {
        return _weaponAim;
    }

    public void SetCurrentWaepon(int weaponIndex)
    {
        currentWeaponIndex = weaponIndex; 
    }

    private void updateGunUI()
    {
        UIController.instance.SetGunImage(_playerGuns[currentWeaponIndex].GetGunUI());
        UIController.instance.SetGunName(_playerGuns[currentWeaponIndex].GetGunName());
    }

    public Gun GetCurrentGun()
    {
        return _playerGuns[currentWeaponIndex];
    }

    public void SetAllowToPlayerMove(bool val)
    {
        _canMove = val;
    }
}
