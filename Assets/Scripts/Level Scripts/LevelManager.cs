using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float _loadTime = 3f;
    [SerializeField] private Transform _startPoint;

    private bool _isPaused = false;

    public static LevelManager instance;

    private int _currentCoins;

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

    private void Start()
    {
        PlayerController.instance.transform.position = _startPoint.position;
        PlayerController.instance.SetAllowToPlayerMove(true);
        _currentCoins = PlayerData.Instance.GetPlayerCurrentCoins();
        setTimeScale(1f);
        updateUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public IEnumerator LoadNextLevel(int nextLevel)
    {
        yield return new WaitForSecondsRealtime(_loadTime);

        SceneManager.LoadScene(nextLevel);
    }

    public void PauseUnpause()
    {
        _isPaused = !_isPaused;
        if (_isPaused)
        {
            UIController.instance.setActivePauseScreen(true);
            setTimeScale(0f);
        }
        else
        {
            UIController.instance.setActivePauseScreen(false);
            setTimeScale(1f);
        }
    }

    public void setTimeScale(float value)
    {
        Time.timeScale = value;
    }

    public void GetCoin(int amount)
    {
        _currentCoins += amount;
        updateUI();
    }

    public bool TrySpendCoin(int amount)
    {
        if(_currentCoins < amount)
        {
            Debug.Log("You have not enough coins!");
            return false;
        }
        else
        {
            SpendCoin(amount);
        }
        return true;
    }
    private void SpendCoin(int amount)
    {
        _currentCoins -= amount;
        updateUI();
    }

    private void updateUI()
    {
        UIController.instance.SetCoins(_currentCoins.ToString());
    }

    public bool GetPause()
    {
        return _isPaused;
    }

    public int GetCurrentCoins()
    {
        return _currentCoins;
    }
}
