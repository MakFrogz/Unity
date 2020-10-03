using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enemy_Scripts;



public class SkeletonController : BaseEnemyController
{
    /*[SerializeField] private Rigidbody2D _enemyBody;
    [SerializeField] private float _enemySpeed;
    [SerializeField] private float _enemyRangeTrigger;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _enemyHealth = 100f;
    [SerializeField] private GameObject[] _deathSplatters;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _fireRate;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _enemyFireRange;
    [SerializeField] private SpriteRenderer _enemySprite;

    private Vector3 _moveDir;
    private float _nextFire = 0f;*/
    void Start()
    {
    }

    void Update()
    {
        /*if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= _enemyRangeTrigger)
        {
            _moveDir = PlayerController.instance.transform.position - transform.position;
        }
        else
        {
            _moveDir = Vector3.zero;
        }
        _moveDir.Normalize();*/
        InputProccess();
    }

    private void FixedUpdate()
    {
        if (_enemySprite.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            Move();
            FlipEnemy();
            Attack();
            return;
        }
        StopMoving();
    }

    /*private void StopMoving()
    {
        _enemyBody.velocity = Vector3.zero;
    }*/
    /*private void Attack()
    {
        if(Time.time >= _nextFire && Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= _enemyFireRange)
        {
            AudioManager.instance.PlaySFX(15);
            Instantiate(_bullet, _firePoint.position, _firePoint.rotation);
            _nextFire = Time.time + 1f / _fireRate;
        }
    }*/

    /*private void Move()
    {
        bool isWalking = _moveDir != Vector3.zero;
        _animator.SetBool("isWalking", isWalking);
        _enemyBody.velocity = _moveDir * _enemySpeed;
    }

    private void FlipEnemy()
    {
        if(PlayerController.instance.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }*/

    /*public void ApplyDamage(float damage)
    {
        AudioManager.instance.PlaySFX(2);
        _animator.SetTrigger("Hurt");
        _enemyHealth -= damage;

        if(_enemyHealth <= 0)
        {
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(1);
            Instantiate(getRandomSplatter(), transform.position, Quaternion.Euler(0f, 0f, getRandomAngle()));
        }
    }*/

   /* private GameObject getRandomSplatter()
    {
        GameObject deathSplatter = _deathSplatters[Random.Range(0, _deathSplatters.Length - 1)];
        return deathSplatter;
    }

    private float getRandomAngle()
    {
        float angle = Random.Range(-180, 180);
        return angle;
    }*/

}
