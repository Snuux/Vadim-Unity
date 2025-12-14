using UnityEngine;

public class TransformRotator
{
    private Transform _transform;
    private float _rotationSpeed;
    private Vector3 _currentDirection;

    public TransformRotator(Transform transform)
    {
        _transform = transform;
    }

    public Quaternion CurrentRotation => _transform.rotation;

    public void Update(float deltaTime)
    {
        if (_currentDirection.magnitude <= 0.01f)
            return;

        Quaternion currentRotation = Quaternion.LookRotation(_currentDirection.normalized);
        float step = _rotationSpeed * deltaTime;

        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, currentRotation, step);
    }

    public void SetRotation(Vector3 direction, float speed)
    {
        _currentDirection = direction;
        _rotationSpeed = speed;
    }
}