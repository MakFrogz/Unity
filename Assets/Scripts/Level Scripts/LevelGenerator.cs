using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _roomPrefab;
    [SerializeField] private int _roomAmount = 10;
    [SerializeField] private Color _startRoomColor;
    [SerializeField] private Color _endRoomColor;
    [SerializeField] private Color _shopRoomColor;
    [SerializeField] private Transform _generatorPoint;
    [SerializeField] private LayerMask _roomLayerMask;
    [SerializeField] private RoomPrefabs _rooms;
    [SerializeField] private RoomCenter _startCenter;
    [SerializeField] private RoomCenter _endCenter;
    [SerializeField] private RoomCenter _shopCenter;
    [SerializeField] private RoomCenter[] _roomCenters;

    private float _xOffset = 18f;
    private float _yOffset = 10f;
    private Direction _direction;
    private List<GameObject> _roomList = new List<GameObject>();
    private List<GameObject> _roomOutlineList = new List<GameObject>();
    private enum Direction { up, down, left, right};
    void Start()
    {

        GameObject startRoom = Instantiate(_roomPrefab, _generatorPoint.position, Quaternion.identity);
        startRoom.GetComponent<SpriteRenderer>().color = _startRoomColor;
        _roomList.Add(startRoom);
        _direction = (Direction)Random.Range(0, 4);
        MoveGeneratorPoint();

        for (int i = 0; i < _roomAmount; i++)
        {
            GameObject newRoom = Instantiate(_roomPrefab, _generatorPoint.position, Quaternion.identity);
            _roomList.Add(newRoom);
            _direction = (Direction) Random.Range(0, 4);
            MoveGeneratorPoint();

            if(i + 1 == _roomAmount)
            {
                newRoom.GetComponent<SpriteRenderer>().color = _endRoomColor;
            }

            while(Physics2D.OverlapCircle(_generatorPoint.position, 0.5f, _roomLayerMask))
            {
                MoveGeneratorPoint();
            }
        }
        int shopIndex = Random.Range(3, 8);
        CreateShopRoom(shopIndex);
        foreach(GameObject room in _roomList)
        {
            CreateOutlineRoom(room.transform.position);
        }
        
        for(int i = 0; i < _roomOutlineList.Count; i++)
        {
            if(i == 0)
            {
                Instantiate(_startCenter, _roomOutlineList[i].transform.position, Quaternion.identity)._roomActivator = _roomOutlineList[i].GetComponent<RoomActivator>(); ;
                continue;
            }

            if (i + 1 == _roomOutlineList.Count)
            {
                Instantiate(_endCenter, _roomOutlineList[i].transform.position, Quaternion.identity)._roomActivator = _roomOutlineList[i].GetComponent<RoomActivator>();
                continue;
            }

            if(i == shopIndex)
            {
                Instantiate(_shopCenter, _roomOutlineList[i].transform.position, Quaternion.identity)._roomActivator = _roomOutlineList[i].GetComponent<RoomActivator>();
                continue;
            }

            CreateRoomCenter(_roomOutlineList[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void MoveGeneratorPoint()
    {
        switch (_direction)
        {
            case Direction.up:
                _generatorPoint.position += new Vector3(0f, _yOffset, 0f);
                break;

            case Direction.down:
                _generatorPoint.position += new Vector3(0f, -_yOffset, 0f);
                break;

            case Direction.left:
                _generatorPoint.position += new Vector3(-_xOffset, 0f, 0f);
                break;

            case Direction.right:
                _generatorPoint.position += new Vector3(_xOffset, 0f, 0f);
                break;
        }
    }

    private void CreateOutlineRoom(Vector3 roomPosition)
    {
        bool isAbove = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, _yOffset, 0f), 0.5f, _roomLayerMask);
        bool isBelow = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, -_yOffset, 0f), 0.5f, _roomLayerMask);
        bool isLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-_xOffset, 0f, 0f), 0.5f, _roomLayerMask);
        bool isRight = Physics2D.OverlapCircle(roomPosition + new Vector3(_xOffset, 0f, 0f), 0.5f, _roomLayerMask);

        int overlapCount = 0;

        if (isAbove)
        {
            overlapCount++;
        }

        if (isBelow)
        {
            overlapCount++;
        }

        if (isLeft)
        {
            overlapCount++;
        }

        if (isRight)
        {
            overlapCount++;
        }

        GameObject outlineRoom = null;

        switch (overlapCount)
        {
            case 0:
                Debug.LogError("Didn`t find exist room!");
                break;
            case 1:
                if (isAbove)
                {
                    outlineRoom = Instantiate(_rooms._roomU, roomPosition, Quaternion.identity);
                }
                if (isBelow)
                {
                    outlineRoom = Instantiate(_rooms._roomD, roomPosition, Quaternion.identity);
                }
                if (isLeft)
                {
                    outlineRoom = Instantiate(_rooms._roomL, roomPosition, Quaternion.identity);
                }
                if (isRight)
                {
                    outlineRoom = Instantiate(_rooms._roomR, roomPosition, Quaternion.identity);
                }
                break;
            case 2:
                if(isAbove && isBelow)
                {
                    outlineRoom = Instantiate(_rooms._roomUD, roomPosition, Quaternion.identity);
                }
                if (isLeft && isRight)
                {
                    outlineRoom = Instantiate(_rooms._roomLR, roomPosition, Quaternion.identity);
                }
                if (isAbove && isLeft)
                {
                    outlineRoom = Instantiate(_rooms._roomUL, roomPosition, Quaternion.identity);
                }
                if (isAbove && isRight)
                {
                    outlineRoom = Instantiate(_rooms._roomUR, roomPosition, Quaternion.identity);
                }
                if (isLeft && isBelow)
                {
                    outlineRoom = Instantiate(_rooms._roomDL, roomPosition, Quaternion.identity);
                }
                if (isRight && isBelow)
                {
                    outlineRoom = Instantiate(_rooms._roomDR, roomPosition, Quaternion.identity);
                }
                break;
            case 3:
                if(isAbove && isBelow && isLeft)
                {
                    outlineRoom = Instantiate(_rooms._roomUDL, roomPosition, Quaternion.identity);
                }
                if (isAbove && isBelow && isRight)
                {
                    outlineRoom = Instantiate(_rooms._roomUDR, roomPosition, Quaternion.identity);
                }
                if (isLeft && isRight && isAbove)
                {
                    outlineRoom = Instantiate(_rooms._roomULR, roomPosition, Quaternion.identity);
                }
                if (isLeft && isRight && isBelow)
                {
                    outlineRoom = Instantiate(_rooms._roomDLR, roomPosition, Quaternion.identity);
                }
                break;
            case 4:
                outlineRoom = Instantiate(_rooms._roomUDLR, roomPosition, Quaternion.identity);
                break;
        }
        _roomOutlineList.Add(outlineRoom);
    }

    private void CreateRoomCenter(GameObject outline)
    {
        int centerIndex = Random.Range(0, _roomCenters.Length);
        Instantiate(_roomCenters[centerIndex], outline.transform.position, Quaternion.identity)._roomActivator = outline.GetComponent<RoomActivator>();
    }

    private void CreateShopRoom(int index)
    {
        /*GameObject shopRoom = _roomList[index];
        shopRoom.GetComponent<SpriteRenderer>().color = _shopRoomColor;
        _roomList.RemoveAt(index);
        _roomList.Insert(index, shopRoom);*/

        _roomList[index].GetComponent<SpriteRenderer>().color = _shopRoomColor;

    }
}


[System.Serializable]
public class RoomPrefabs
{
    public GameObject _roomU;
    public GameObject _roomD;
    public GameObject _roomL;
    public GameObject _roomR;

    public GameObject _roomUD;
    public GameObject _roomLR;
    public GameObject _roomUR;
    public GameObject _roomUL;
    public GameObject _roomDR;
    public GameObject _roomDL;

    public GameObject _roomUDR;
    public GameObject _roomUDL;
    public GameObject _roomULR;
    public GameObject _roomDLR;

    public GameObject _roomUDLR;


}
