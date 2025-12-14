using MobaLesson;
using UnityEngine;

public class LongIdleStateController : Controller
{
    private AgentCharacter _agentCharacter;
    private Controller _aiController;

    private float _requiredTimeToLongIdle;
    private float _currentTimeToLongIdle;

    public LongIdleStateController(AgentCharacter agent, float requiredTimeToLongIdle, Controller randomPointsController)
    {
        _agentCharacter = agent;
        _requiredTimeToLongIdle = requiredTimeToLongIdle;
        _aiController = randomPointsController;

        _aiController.Enable();
    }

    public override void Enable()
    {
        base.Enable();
        _aiController.Enable();
    }

    public override void Disable()
    {
        base.Disable();
        _aiController.Disable();
    }

    public override void UpdateLogic(float deltaTime)
    {
        UpdateIsLongIdle(deltaTime);

        if (IsLongIdle() && _agentCharacter.HasPath() == false)
            _aiController.Enable();
        else
            _aiController.Disable();

        _aiController.Update(Time.deltaTime);
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