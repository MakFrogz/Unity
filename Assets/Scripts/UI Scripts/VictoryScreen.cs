using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private float _waitTimeToPressAnyKey;
    [SerializeField] private GameObject _anyKeyText;

    private int _mainMenu = 0;

    // Update is called once per frame
    void Update()
    {
        if (_waitTimeToPressAnyKey > 0)
        {
            _waitTimeToPressAnyKey -= Time.deltaTime;
            if (_waitTimeToPressAnyKey <= 0)
            {
                _anyKeyText.SetActive(true);
            }
        }
        else
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(_mainMenu);
            }
        }
    }
}
