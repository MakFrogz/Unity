using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamageController : MonoBehaviour
{
    [SerializeField] private float _damageRate = 1f;

    private bool isAttack = false;
    private float _nextTimeAttack = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isAttack && Time.time >= _nextTimeAttack)
        {
            _nextTimeAttack = Time.time + 1f / _damageRate;
            PlayerHealthController.instance.ApplyDamage();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isAttack = false;
        }
    }
}
