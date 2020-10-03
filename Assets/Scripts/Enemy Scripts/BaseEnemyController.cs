using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemy_Scripts
{
    public abstract class BaseEnemyController : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D _enemyBody;
        [SerializeField] protected float _enemySpeed;
        [SerializeField] protected float _enemyRangeTrigger;
        [SerializeField] protected Animator _animator;
        [SerializeField] protected float _enemyHealth = 100f;
        [SerializeField] protected GameObject[] _deathSplatters;
        [SerializeField] protected GameObject _bullet;
        [SerializeField] protected float _fireRate;
        [SerializeField] protected Transform _firePoint;
        [SerializeField] protected float _enemyFireRange;
        [SerializeField] protected SpriteRenderer _enemySprite;
        [SerializeField] private bool _isDropItems;
        [SerializeField] private float _chanceToDropItem;
        [SerializeField] private GameObject[] _dropItems;

        protected Vector3 _moveDir;
        protected float _nextFire = 0f;


        protected virtual void InputProccess()
        {
            _moveDir = Vector3.zero;
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= _enemyRangeTrigger)
            {
                _moveDir = PlayerController.instance.transform.position - transform.position;
            }
            _moveDir.Normalize();
        }

        protected void Move()
        {
            bool isWalking = _moveDir != Vector3.zero;
            _animator.SetBool("isWalking", isWalking);
            _enemyBody.velocity = _moveDir * _enemySpeed;
        }

        protected void StopMoving()
        {
            _enemyBody.velocity = Vector3.zero;
        }

        protected void FlipEnemy()
        {
            if (PlayerController.instance.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                transform.localScale = Vector3.one;
            }
        }

        protected virtual void Attack()
        {
            if (Time.time >= _nextFire && Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= _enemyFireRange)
            {
                AudioManager.instance.PlaySFX(15);
                Instantiate(_bullet, _firePoint.position, _firePoint.rotation);
                _nextFire = Time.time + 1f / _fireRate;
            }
        }

        public void ApplyDamage(float damage)
        {
            AudioManager.instance.PlaySFX(2);
            _animator.SetTrigger("Hurt");
            _enemyHealth -= damage;

            if (_enemyHealth <= 0)
            {
                Destroy(gameObject);
                AudioManager.instance.PlaySFX(1);
                Instantiate(getRandomSplatter(), transform.position, Quaternion.Euler(0f, 0f, getRandomAngle()));

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
        }

        private GameObject getRandomSplatter()
        {
            GameObject deathSplatter = _deathSplatters[Random.Range(0, _deathSplatters.Length - 1)];
            return deathSplatter;
        }

        private float getRandomAngle()
        {
            float angle = Random.Range(-180, 180);
            return angle;
        }
    }
}
