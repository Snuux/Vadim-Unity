public abstract class Controller
{
    private bool _isEnabled;

    public virtual void Enable() => _isEnabled = true;

    public virtual void Disable() => _isEnabled = false;

    public void Update(float deltaTime)
    {
        if (_isEnabled == false)
            return;

        UpdateControlling(deltaTime);
    }

    public abstract void UpdateControlling(float deltaTime);
}
