using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{

    [SerializeField] private GameObject[] _pieces;
    [SerializeField] private int _maxPieces;
    [SerializeField] private float _chanceToDropItem;
    [SerializeField] private GameObject[] _dropItems;
    [SerializeField] private bool _isDropItems;

    private void Smash()
    {
        Destroy(gameObject);

        AudioManager.instance.PlaySFX(0);

        int pieceCount = Random.Range(1, _pieces.Length);

        for (int i = 0; i < pieceCount; i++)
        {
            GameObject piece = _pieces[Random.Range(0, _pieces.Length)];
            Instantiate(piece, transform.position, transform.rotation);
        }

        if (_isDropItems)
        {
            float dropChance = Random.Range(0f, 100f);

            if (dropChance < _chanceToDropItem)
            {
                GameObject item = _dropItems[Random.Range(0, _dropItems.Length)];
                Instantiate(item, transform.position, transform.rotation);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" )
        {
            if (PlayerController.instance.getDisableDashTime() > Time.time)
            {
                Smash();
            }
        }

        if (collision.tag == "PlayerBullet")
        {
            Smash();
        }
    }
}
