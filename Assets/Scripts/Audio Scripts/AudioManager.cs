using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource _levelMusic;
    [SerializeField] private AudioSource _gameOverMusic;
    [SerializeField] private AudioSource _victoryMusic;
    [SerializeField] private AudioSource[] _sfx;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGameOver()
    {
        _levelMusic.Stop();
        _gameOverMusic.Play();
    }
    
    public void PlayVictory()
    {
        _levelMusic.Stop();
        _victoryMusic.Play();
    }

    public void PlaySFX(int sfxIndex)
    {
        if(_sfx[sfxIndex] != null)
        {
            _sfx[sfxIndex].Stop();
            _sfx[sfxIndex].Play();
        }
    }
}
