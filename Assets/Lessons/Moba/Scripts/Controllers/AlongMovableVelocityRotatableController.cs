
namespace MobaLesson
{
    public class AlongMovableVelocityRotatableController : Controller
    {
        private IDirectionalMovable _movable;
        private IDirectionalRotatable _rotatable;

        public AlongMovableVelocityRotatableController(IDirectionalMovable movable, IDirectionalRotatable rotatable)
        {
            _movable = movable;
            _rotatable = rotatable;
        }

        public override void UpdateLogic(float deltaTime)
        {
            _rotatable.SetRotationDirection(_movable.CurrentVelocity);
        }
    }
}