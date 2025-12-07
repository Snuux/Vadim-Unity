using UnityEngine;
using UnityEngine.AI;

namespace MobaLesson
{
    public class AgentCharacter : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _rotationSpeed;

        [SerializeField] private float _moveSpeed;

        private AgentMover _mover;
        private NavMeshAgent _agent;
        private DirectionalRotator _rotator;

        public Vector3 CurrentVelocity => _agent.desiredVelocity;
        //public AgentMover Mover => _mover;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;

            _rotator = new DirectionalRotator(transform, _rotationSpeed);
            _mover = new AgentMover(_agent, _moveSpeed);
        }

        private void Update()
        {
            _rotator.Update(Time.deltaTime);
        }

        public void SetDestination(Vector3 position) => _agent.SetDestination(_target.position);

        public void StopMove() => _mover.Stop();

        public void ResumeMove() => _mover.Resume();

        public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);

        public bool TryGetPath(Vector3 targetPosition, NavMeshPath pathToTarget)
            => NavMeshUtils.TryGetPath(_agent, targetPosition, pathToTarget);
    }
}