using UnityEngine;

namespace MobaLesson
{
    public class PlayerDirectionMovableController : Controller
    {
        IDirectionalMovable _movable;

        public PlayerDirectionMovableController(IDirectionalMovable movable)
        {
            _movable = movable;
        }

        public override void UpdateLogic(float deltaTime)
        {
            Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            //inputDirection = Quaternion.Euler(0, 45, 0) * inputDirection;

            _movable.SetMoveDirection(inputDirection);
        }
    }
}