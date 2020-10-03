using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enemy_Scripts;

public class FireController : BaseEnemyController
{
    [SerializeField] private float _attackRate = 1f;
    [SerializeField] private Transform[] points;

    private int currentPoint = 0;
    private bool _isCollision = false;
    private float _nextTimeAttack = 0f;

    void Start()
    {
        
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

    protected override void Attack()
    {
        if (_isCollision && Time.time >= _nextTimeAttack)
        {
            PlayerHealthController.instance.ApplyDamage();
            _nextTimeAttack = Time.time + 1f / _attackRate;
        }
    }

    protected override void InputProccess()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= _enemyRangeTrigger)
        {
            _moveDir = PlayerController.instance.transform.position - transform.position;
        }
        else
        {
            _moveDir = points[currentPoint].position - transform.position;
            if(Vector3.Distance(points[currentPoint].position, transform.position) <= 0.2f)
            {
                currentPoint++;
                if(currentPoint >= points.Length)
                {
                    currentPoint = 0;
                }
            }
        }
        _moveDir.Normalize();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
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
