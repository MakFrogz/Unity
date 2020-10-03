using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _cameraSpeed = 5f;
    [SerializeField] private Transform _target;
    [SerializeField] private Camera _fullMapCamera;

    private Camera _mainCamera;
    private bool _isFullMap = false;

    public static CameraController instance = null;

    private void Awake()
    {
        /*if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);*/
        instance = this;
    }
    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(_target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_target.position.x, _target.position.y, transform.position.z), _cameraSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.M) && !LevelManager.instance.GetPause())
        {
            _isFullMap = !_isFullMap;
            OpenFullMap();
        }
    }

    public void setTarget(Transform target)
    {
        _target = target;
    }

    private void OpenFullMap()
    {
        _fullMapCamera.enabled = _isFullMap;
        UIController.instance.SetMiniMap(!_isFullMap);
    }

    public Camera GetMainCamera()
    {
        return _mainCamera;
    }
}
