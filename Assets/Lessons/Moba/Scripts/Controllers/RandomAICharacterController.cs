using UnityEngine;

namespace MobaLesson
{
    public class RandomAICharacterController : Controller
    {
        private IDirectionalMovable _movable;

        private float _time;
        private float _timeToChangeDirection;

        private Vector3 _inputDirection;

        public RandomAICharacterController(IDirectionalMovable character, float timeToChangeDirection)
        {
            _movable = character;
            _timeToChangeDirection = timeToChangeDirection;
        }

        public override void UpdateLogic(float deltaTime)
        {
            _time += deltaTime;

            if (_time > _timeToChangeDirection)
            {
                _inputDirection = new UnityEngine.Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
                _time = 0;
            }

            _movable.SetMoveDirection(_inputDirection);
        }
    }
}