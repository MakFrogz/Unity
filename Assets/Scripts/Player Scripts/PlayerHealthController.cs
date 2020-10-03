using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] private int _playerMaxHealth;
    [SerializeField] private float _invulnerabilityTime = 0.5f;
    public GameObject _session;

    private int _playerCurrentHealth;
    public static PlayerHealthController instance = null;
    private float _disableInvulnerabilityTime = 0f;
    private Color _colorPlayerAfterHitting;
    private void Awake()
    {
        /*if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);*/
        instance = this;
    }
    void Start()
    {
        _playerMaxHealth = PlayerData.Instance.GetPlayerMaxHealth();
        _playerCurrentHealth = PlayerData.Instance.GetPlayerCurrentHealth();
        //_playerCurrentHealth = _playerMaxHealth;
        //UIController.instance.setMaxSliderHealthValue(_playerMaxHealth);
        //updateUI();
        _colorPlayerAfterHitting = PlayerController.instance.getPlayerSpriteColor();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= _disableInvulnerabilityTime)
        {
            setPlayerSpriteAlpha(1f);
        }
    }

    public void ApplyDamage()
    {
        if (Time.time >= _disableInvulnerabilityTime)
        {
            AudioManager.instance.PlaySFX(11);
            setInvulnerabilityTime(_invulnerabilityTime);
            _playerCurrentHealth--;
            updateUI();
            if (_playerCurrentHealth <= 0)
            {
                PlayerController.instance.gameObject.SetActive(false);
                UIController.instance.getDeathScreen().SetActive(true);
                AudioManager.instance.PlayGameOver();
                Destroy(_session);
            }

        }
    }

    public void HealPlayer(int healAmount)
    {
        if (_playerCurrentHealth < _playerMaxHealth)
        {
            _playerCurrentHealth += healAmount;
            updateUI();
        }
    }

    private void setPlayerSpriteAlpha(float alpha)
    {
        _colorPlayerAfterHitting.a = alpha;
        PlayerController.instance.setPlayerSpriteColor(_colorPlayerAfterHitting);
    }

    public void setInvulnerabilityTime(float time)
    {
        setPlayerSpriteAlpha(0.5f);
        _disableInvulnerabilityTime = Time.time + time;
    }

    private void updateUI()
    {
        UIController.instance.setCurrentSliderHealthValue(_playerCurrentHealth);
        UIController.instance.setHealthText(string.Format("{0} / {1}", _playerCurrentHealth, _playerMaxHealth));
    }

    public void RestoreHealth()
    {
        _playerCurrentHealth = _playerMaxHealth;
        updateUI();
    }

    public void IncreaseMaxPlayerHealth(int health)
    {
        _playerMaxHealth += health;
        UIController.instance.setMaxSliderHealthValue(_playerMaxHealth);
        updateUI();
    }

    public int GetMaxPlayerHealth()
    {
        return _playerMaxHealth;
    }

    public int GetCurrentPlayerHealth()
    {
        return _playerCurrentHealth;
    }
}
