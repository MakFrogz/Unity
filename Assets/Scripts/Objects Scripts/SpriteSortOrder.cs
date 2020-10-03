using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSortOrder : MonoBehaviour
{
    private SpriteRenderer _objectSprite;
    void Start()
    {
        _objectSprite = GetComponent<SpriteRenderer>();
        _objectSprite.sortingOrder = Mathf.RoundToInt(transform.position.y * -10f);
    }

}
