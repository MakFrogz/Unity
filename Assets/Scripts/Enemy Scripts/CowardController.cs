using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enemy_Scripts;

public class CowardController : BaseEnemyController
{
    [SerializeField] private float _runAwayRange = 3f;
    void Start()
    {
        
    }

    void Update()
    {
        RunAway();
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

    private void RunAway()
    {
        if(Vector3.Distance(PlayerController.instance.transform.position, transform.position) <= _runAwayRange)
        {
            _moveDir = transform.position - PlayerController.instance.transform.position;
        }
        else
        {
            _moveDir = Vector3.zero;
        }
        _moveDir.Normalize();
    }
}
