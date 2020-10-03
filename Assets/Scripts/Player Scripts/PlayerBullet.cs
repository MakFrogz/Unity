using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using Assets.Scripts.Enemy_Scripts;

public class PlayerBullet : MonoBehaviour
{

    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Rigidbody2D _bulletBody;
    [SerializeField] private GameObject _impactEffect;
    [SerializeField] private float _bulletDamage = 30f;

    void FixedUpdate()
    {
        _bulletBody.velocity = transform.right * _bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(_impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<BaseEnemyController>().ApplyDamage(_bulletDamage);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
