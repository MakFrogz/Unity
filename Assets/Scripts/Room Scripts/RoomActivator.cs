using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomActivator : MonoBehaviour
{
    [SerializeField] private GameObject[] _doors;
    [SerializeField] private GameObject _mapHider;
    public bool RoomActive { get; private set; } 
    public bool _shoudCloseDoors { get; set; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            CameraController.instance.setTarget(transform);
            RoomActive = true;
            if (_shoudCloseDoors)
            {
                CloseDoors();
            }

            _mapHider.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            RoomActive = false;
        }
    }

    private void setDoors(bool value)
    {
        foreach (GameObject door in _doors)
        {
            door.SetActive(value);
        }
    }

    public void OpenDoors()
    {
        setDoors(false);
        _shoudCloseDoors = false;
    }

    private void CloseDoors()
    {
        setDoors(true);
    }
}
