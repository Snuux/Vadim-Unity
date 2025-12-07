using UnityEngine;

namespace MobaLesson
{
    public class PlayerDirectionRotatableController : Controller
    {
        IDirectionalRotatable _rotatable;

        public PlayerDirectionRotatableController(IDirectionalRotatable rotatable)
        {
            _rotatable = rotatable;
        }

        public override void UpdateLogic(float deltaTime)
        {
            Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            inputDirection = Quaternion.Euler(0, 45, 0) * inputDirection * deltaTime;

            _rotatable.SetRotationDirection(inputDirection);
        }
    }
}