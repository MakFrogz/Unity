using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceDrop : MonoBehaviour
{
    [SerializeField] private float _pieceSpeed = 3f;
    [SerializeField] private float _deceleration = 4f;
    [SerializeField] private float _lifeTime = 3f;
    [SerializeField] private SpriteRenderer _pieceSprite;
    [SerializeField] private float _fadeSpeed = 2f;


    private Color _pieceColor;
    private Vector3 _moveDirection;
    void Start()
    {
        _moveDirection.x = Random.Range(-_pieceSpeed, _pieceSpeed);
        _moveDirection.y = Random.Range(-_pieceSpeed, _pieceSpeed);
        _pieceColor = _pieceSprite.color;
    }

    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        if(_lifeTime <= 0)
        {
            _pieceColor.a = Mathf.MoveTowards(_pieceColor.a, 0f, _fadeSpeed * Time.deltaTime);
            _pieceSprite.color = _pieceColor;
            if(_pieceColor.a == 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        transform.position += _moveDirection * Time.fixedDeltaTime;  
        _moveDirection = Vector3.Lerp(_moveDirection, Vector3.zero, _deceleration * Time.fixedDeltaTime);
    }
}
