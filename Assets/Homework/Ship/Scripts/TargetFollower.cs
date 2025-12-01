using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    private const float FollowSpeed = 2.0f;

    [SerializeField] private Transform _targetTransform;
    private Vector3 _defaultOffset;

    private void Awake()
    {
        _defaultOffset = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _targetTransform.position + _defaultOffset, Time.deltaTime * FollowSpeed);
    }
}
