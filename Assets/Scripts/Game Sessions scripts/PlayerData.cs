using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private static PlayerData _instance;
    public static PlayerData Instance { get { return _instance; } }

    [SerializeField] private int _playerMaxHealth;
    [SerializeField] private int _playerCurrentHealth;
    [SerializeField] private int _playerCurrentCoins;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetPlayerMaxHealth(int maxHealth)
    {
        _playerMaxHealth = maxHealth;
    }

    public int GetPlayerMaxHealth()
    {
        return _playerMaxHealth;
    }

    public void SetPlayerCurrentHealth(int currentHealth)
    {
        _playerCurrentHealth = currentHealth;
    }

    public int GetPlayerCurrentHealth()
    {
        return _playerCurrentHealth;
    }

    public void SetPlayerCurrentCoins(int coins)
    {
        _playerCurrentCoins = coins;
    }

    public int GetPlayerCurrentCoins()
    {
        return _playerCurrentCoins;
    }
}
