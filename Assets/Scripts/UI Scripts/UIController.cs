using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Text _healthText;
    [SerializeField] private Text _coinsText;
    [SerializeField] private GameObject _deathScreeen;
    [SerializeField] private Image _fadeImage;
    [SerializeField] private float _fadeSpeed;
    [SerializeField] private int _mainMenuScene;
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private GameObject _mapImage;
    [SerializeField] private Image _imageGun;
    [SerializeField] private Text _gunNameText;
    [SerializeField] private GameObject _session;

    private int _newGameScene;
    private float _targetFade;

    public static UIController instance = null;

    private void Awake()
    {
        /*if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);*/
        instance = this;
    }
    void Start()
    {
        Debug.Log("Player max health: " + PlayerHealthController.instance.GetMaxPlayerHealth());
        Debug.Log("Player current health: " + PlayerHealthController.instance.GetCurrentPlayerHealth());
        /*setMaxSliderHealthValue(PlayerHealthController.instance.GetMaxPlayerHealth());
        setCurrentSliderHealthValue(PlayerHealthController.instance.GetCurrentPlayerHealth());*/
        setMaxSliderHealthValue(PlayerData.Instance.GetPlayerMaxHealth());
        setCurrentSliderHealthValue(PlayerData.Instance.GetPlayerCurrentHealth());
        setHealthText(string.Format("{0} / {1}", PlayerData.Instance.GetPlayerCurrentHealth(), PlayerData.Instance.GetPlayerMaxHealth()));
        SetCoins(PlayerData.Instance.GetPlayerCurrentCoins().ToString());
        SetGunImage(PlayerController.instance.GetCurrentGun().GetGunUI());
        SetGunName(PlayerController.instance.GetCurrentGun().GetGunName());
        setFade(0f);
        //_newGameScene = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        if(_fadeImage.color.a != _targetFade)
        {
            _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, Mathf.MoveTowards(_fadeImage.color.a, _targetFade, _fadeSpeed * Time.deltaTime));
        }
    }


    public void setMaxSliderHealthValue(int health)
    {
        _healthSlider.maxValue = health;
    }

    public void setCurrentSliderHealthValue(int health)
    {
        _healthSlider.value = health;
    }

    public void setHealthText(string healthString)
    {
        _healthText.text = healthString;
    }

    public GameObject getDeathScreen()
    {
        return _deathScreeen;
    }

    public void setActivePauseScreen(bool pause)
    {
        _pauseScreen.SetActive(pause);
    }

    public void setFade(float target)
    {
        _targetFade = target;
    }


    public void NewGame()
    {
        SceneManager.LoadScene(_newGameScene);
    }

    public void ReturnToMainMenu()
    {
        Destroy(_session);
        SceneManager.LoadScene(_mainMenuScene);
    }

    public void Resume()
    {
        LevelManager.instance.PauseUnpause();
    }

    public void SetCoins(string stringCoins)
    {
        _coinsText.text = stringCoins;
    }

    public void SetMiniMap(bool val)
    {
        _mapImage.SetActive(val);
    }

    public void SetGunImage(Sprite gunSprite)
    {
        _imageGun.sprite = gunSprite;
    }

    public void SetGunName(string gunName)
    {
        _gunNameText.text = gunName;
    }
}
