using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enemy_Scripts;

public class SlimeController : BaseEnemyController
{
    [SerializeField] private float _wanderingTime;
    [SerializeField] private float _pauseTime;
    [SerializeField] private float _attackRate = 1f;

    private float _wanderingCounter;
    private float _pauseCounter;
    private Vector3 _wanderDirection;
    private bool _isCollision = false;
    private float _nextTimeAttack = 0f;

    void Start()
    {
        _pauseCounter = Random.Range(_pauseTime * 0.75f, _pauseTime * 1.5f);
    }

    void Update()
    {
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

    protected override void InputProccess()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= _enemyRangeTrigger)
        {
            _moveDir = PlayerController.instance.transform.position - transform.position;
        }
        else
        {
            Wandering();
        }
        _moveDir.Normalize();
    }

    private void Wandering()
    {
        if(_wanderingCounter > 0)
        {
            _wanderingCounter -= Time.deltaTime;
            if(_wanderingCounter <= 0)
            {
                _pauseCounter = Random.Range(_pauseTime * 0.25f, _pauseTime * 1.25f);
            }
        }

        if(_pauseCounter > 0)
        {
            _pauseCounter -= Time.deltaTime;
            if(_pauseCounter <= 0)
            {
                _wanderingCounter = Random.Range(_wanderingTime * 0.25f, _wanderingTime * 1.25f);
                _moveDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            }
        }
    }

    protected override void Attack()
    {
        if (_isCollision && Time.time >= _nextTimeAttack)
        {
            PlayerHealthController.instance.ApplyDamage();
            _nextTimeAttack = Time.time + 1f / _attackRate;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _isCollision = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isCollision = false;
        }
    }
}
