public class AgentRotatableController : Controller
{
    private IAgentMovable _movable;
    private IAgentRotatable _rotatable;

    public AgentRotatableController(IAgentMovable movable, IAgentRotatable rotatable)
    {
        _movable = movable;
        _rotatable = rotatable;
    }

    public override void UpdateControlling(float deltaTime)
    {
        _rotatable.SetRotationDirection(_movable.CurrentVelocity);
    }
}