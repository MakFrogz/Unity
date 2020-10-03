using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCenter : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();
    [SerializeField] private bool _shoudKillAllEnemies;

    public RoomActivator _roomActivator { get; set; }
    void Start()
    {
        if (_shoudKillAllEnemies)
        {
            _roomActivator._shoudCloseDoors = true;
        }
    }

    void Update()
    {
        if(_roomActivator == null)
        {
            return;
        }

        if (_roomActivator.RoomActive && _shoudKillAllEnemies && _enemies.Count > 0)
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                if (_enemies[i] == null)
                {
                    _enemies.RemoveAt(i);
                    i--;
                }
            }


            if (_enemies.Count == 0)
            {
                _roomActivator.OpenDoors();
            }
        }
    }
}
