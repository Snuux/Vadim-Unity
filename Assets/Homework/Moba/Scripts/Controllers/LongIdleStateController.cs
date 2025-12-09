using MobaLesson;

public class LongIdleStateController : Controller
{
    private AgentCharacter _agentCharacter;
    Controller _randomPointsController;

    private float _requiredTimeToLongIdle;
    private float _currentTimeToLongIdle;

    public LongIdleStateController(AgentCharacter agent, float requiredTimeToLongIdle, Controller randomPointsController)
    {
        _agentCharacter = agent;
        _requiredTimeToLongIdle = requiredTimeToLongIdle;
        _randomPointsController = randomPointsController;
    }

    public override void UpdateControlling(float deltaTime)
    {
        UpdateIsLongIdle(deltaTime);

        if (IsLongIdle() && _agentCharacter.HasPath() == false)
            _randomPointsController.Enable();
        else
            _randomPointsController.Disable();
    }

    private void UpdateIsLongIdle(float deltaTime)
    {
        if (_agentCharacter.HasPath() == false)
            _currentTimeToLongIdle += deltaTime;
        else
            _currentTimeToLongIdle = 0;
    }

    private bool IsLongIdle() => _currentTimeToLongIdle >= _requiredTimeToLongIdle;
}