using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunChest : MonoBehaviour
{
    [SerializeField] private GunPickUp[] _guns;
    [SerializeField] private Sprite _openChestSprite;
    [SerializeField] private Text _notification;
    [SerializeField] private Transform _dropPoint;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private bool _canOpen;
    private bool _isOpen = false;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (_canOpen)
        {
            if (Input.GetKeyDown(KeyCode.E) && !_isOpen)
            {
                DropRandomGun();
                _spriteRenderer.sprite = _openChestSprite;
                transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                _notification.gameObject.SetActive(false);
                _isOpen = true;
            }
        }
        transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, Time.deltaTime * 3f);
    }

    private void DropRandomGun()
    {
        int gunIndex = Random.Range(0, _guns.Length);
        Instantiate(_guns[gunIndex], _dropPoint.position, _dropPoint.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _canOpen = true;
            if (!_isOpen)
            {
                _notification.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _canOpen = false;
            _notification.gameObject.SetActive(false);
        }
    }
}
