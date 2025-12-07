using UnityEngine;

namespace MobaLesson
{
    public class Tower : MonoBehaviour, IDirectionalRotatable
    {
        private DirectionalRotator _rotator;

        [SerializeField] private float _rotationSpeed;

        public Quaternion CurrentRotation => _rotator.CurrentRotation;
        public Vector3 Position => transform.position;

        private void Awake()
        {
            _rotator = new DirectionalRotator(transform, _rotationSpeed);
        }

        private void Update()
        {
            _rotator.Update(Time.deltaTime);
        }

        public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);
    }
}