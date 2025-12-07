using UnityEngine;

namespace MobaLesson
{
    public class PlayerCharacterController : Controller
    {
        private Character _character;

        public PlayerCharacterController(Character character)
        {
            _character = character;
        }

        public override void UpdateLogic(float deltaTime)
        {
            Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            inputDirection = Quaternion.Euler(0, 45, 0) * inputDirection;

            _character.SetMoveDirection(inputDirection);
            _character.SetRotationDirection(inputDirection);
        }
    }
}