using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] private int _coinValue = 1;
    [SerializeField] private float _waitTime = 0.5f;

    private float _nextTimePickUp = 0f;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && Time.time >= _nextTimePickUp)
        {
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(5);
            LevelManager.instance.GetCoin(_coinValue);
            _nextTimePickUp = Time.time + _waitTime;
        }
    }
}
