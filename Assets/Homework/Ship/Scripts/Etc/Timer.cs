using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _TargetTime = 0;
    private float _currentTime = 0;

    public float CurrentTime => _currentTime;
    public float LimitTime => _TargetTime;

    private void Update()
    {
        ProcessTimer();
    }

    private void ProcessTimer()
    {
        if (_currentTime < _TargetTime)
            _currentTime += Time.deltaTime;
        else
            _currentTime = _TargetTime;
    }

    public bool IsOver() => _currentTime >= _TargetTime;

    public void ResetTimer() => _currentTime = 0;
}
