using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            AudioManager.instance.PlayVictory();
            PlayerController.instance.setMove(false);
            UIController.instance.setFade(1f);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            PlayerData.Instance.SetPlayerMaxHealth(PlayerHealthController.instance.GetMaxPlayerHealth());
            PlayerData.Instance.SetPlayerCurrentHealth(PlayerHealthController.instance.GetCurrentPlayerHealth());
            PlayerData.Instance.SetPlayerCurrentCoins(LevelManager.instance.GetCurrentCoins());
            StartCoroutine(LevelManager.instance.LoadNextLevel(SceneManager.GetActiveScene().buildIndex));
        }
    }
}
