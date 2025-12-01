using UnityEngine;

public class Wind : MonoBehaviour
{
    private const float RotationSpeed = 2.0f;

    [SerializeField] private bool _isRandomWindDirection;
    [SerializeField] private Timer _changeWindDirectionTimer;

    [SerializeField] private float _windDirectionAngle;
    [SerializeField] private float _strength;

    private Quaternion _targetRotation;

    public float WindDirectionAngle { get => _windDirectionAngle; }
    public float Strength { get => _strength; }

    private void Awake()
    {
        _targetRotation = Quaternion.identity;
    }

    private void Update()
    {
        if (_isRandomWindDirection)
        {
            if (_changeWindDirectionTimer.IsOver())
            {
                _targetRotation = Quaternion.Euler(0, Random.Range(-360, 360), 0);
                _changeWindDirectionTimer.ResetTimer();
            }
        }
        else
        {
            _targetRotation = Quaternion.Euler(0f, _windDirectionAngle, 0f);
        }

        //transform.rotation = _targetRotation;

        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, Time.deltaTime * RotationSpeed);
    }
}
