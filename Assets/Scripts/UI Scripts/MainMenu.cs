using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int _levelToLoad;

    public void StartGame()
    {
        SceneManager.LoadScene(_levelToLoad);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
