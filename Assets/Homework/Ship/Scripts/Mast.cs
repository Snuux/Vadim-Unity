using UnityEngine;

public class Mast : MonoBehaviour
{
    private const float RotationSpeed = 20.0f;

    [SerializeField] private float _rotationSpeed;
    private float _angle;

    public float Angle { get => _angle; }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
            _angle -= _rotationSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.S))
            _angle += _rotationSpeed * Time.deltaTime;

        _angle = Mathf.Clamp(_angle, -90f, 90f);

        //transform.localRotation = Quaternion.Euler(0, _angle, 0);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, _angle, 0), RotationSpeed * Time.deltaTime);
    }


}
