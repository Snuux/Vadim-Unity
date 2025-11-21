using UnityEngine;
using Cinemachine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] _cameras;
    private int _currentCameraIndex = 0;

    void Awake()
    {
        for (int i = 0; i < _cameras.Length; i++)
            _cameras[i].gameObject.SetActive(i == _currentCameraIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _cameras[_currentCameraIndex].gameObject.SetActive(false);
            _currentCameraIndex = (_currentCameraIndex + 1) % _cameras.Length;
            _cameras[_currentCameraIndex].gameObject.SetActive(true);
        }
    }
}
