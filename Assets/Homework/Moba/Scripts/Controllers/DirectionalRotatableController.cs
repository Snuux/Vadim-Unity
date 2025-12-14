public class DirectionalRotatableController : Controller
{
    private IAgentMovable _movable;
    private IDirectionRotatable _rotatable;

    public DirectionalRotatableController(IAgentMovable movable, IDirectionRotatable rotatable)
    {
        _movable = movable;
        _rotatable = rotatable;
    }

    public override void UpdateLogic(float deltaTime)
    {
        _rotatable.SetRotationDirection(_movable.CurrentVelocity);
    }
}