using System;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private const float RotationSpeed = 20.0f;

    [SerializeField] private Wind _wind;
    [SerializeField] private Mast _mast;

    [SerializeField] private float _rotationSpeed;

    private float _angle;

    private float _currentSpeed;

    private void Update()
    {
        ProcessRotation();
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        Vector3 shipDirection = transform.forward; 
        Vector3 mastDirection = _mast.transform.forward;
        Vector3 windDirection = _wind.transform.forward;

        float dotMastWind = Vector3.Dot(mastDirection, windDirection);
        dotMastWind = Mathf.Clamp01(dotMastWind);

        _currentSpeed = _wind.Strength * dotMastWind;

        float dotMastShip = Vector3.Dot(mastDirection, shipDirection);
        dotMastWind = Mathf.Clamp01(dotMastWind);

        _currentSpeed *= dotMastShip;

        Vector3 resultDirection = shipDirection * _currentSpeed * Time.deltaTime;
        //transform.position = Vector3.Lerp(transform.position, transform.position + resultDirection, Time.deltaTime);
        transform.position += resultDirection;


    }

    private void OnDrawGizmos()
    {
        Vector3 shipDirection = transform.forward;
        Vector3 mastDirection = _mast.transform.forward;
        Vector3 windDirection = _wind.transform.forward;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, shipDirection * 6);

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(_mast.transform.position, mastDirection * 2);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(_wind.transform.position, windDirection * 2);

    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.Q))
            _angle -= _rotationSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
            _angle += _rotationSpeed * Time.deltaTime;

        //_angle = Mathf.Clamp(_angle, -90f, 90f);

        //transform.rotation = Quaternion.Euler(0, _angle, 0);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, _angle, 0), RotationSpeed * Time.deltaTime);

    }
}
