using UnityEngine;

namespace MobaLesson
{
    public class Character : MonoBehaviour, IDirectionalMovable, IDirectionalRotatable
    {
        private DirectionalMover _mover;
        private DirectionalRotator _rotator;

        private DirectionalMover _gravityMover;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;

        public Vector3 CurrentVelocity => _mover.CurrentVelocity;
        public Quaternion CurrentRotation => _rotator.CurrentRotation;
        public Vector3 Position => transform.position;

        private void Awake()
        {
            _mover = new DirectionalMover(GetComponent<CharacterController>(), _moveSpeed);
            _rotator = new DirectionalRotator(transform, _rotationSpeed);

            _gravityMover = new DirectionalMover(GetComponent<CharacterController>(), 10);
        }

        private void Update()
        {
            _gravityMover.SetInputDirection(Vector3.down);

            _mover.Update(Time.deltaTime);
            _rotator.Update(Time.deltaTime);
            _gravityMover.Update(Time.deltaTime);
        }

        public void SetMoveDirection(Vector3 inputDirection) => _mover.SetInputDirection(inputDirection);
        public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);
    }

}