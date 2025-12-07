using UnityEngine;
using UnityEngine.AI;

namespace MobaLesson
{
    public class AgentPlayerCharacter : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _moveSpeed;

        private AgentMover _mover;
        private NavMeshAgent _agent;

        public Vector3 CurrentVelocity => _agent.desiredVelocity;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;

            _mover = new AgentMover(_agent, _moveSpeed);
        }

        private void Update()
        {
        }

        public void SetDestination(Vector3 position) => _agent.SetDestination(_target.position);

        public void StopMove() => _mover.Stop();

        public void ResumeMove() => _mover.Resume();

        public bool TryGetPath(Vector3 targetPosition, NavMeshPath pathToTarget)
            => NavMeshUtils.TryGetPath(_agent, targetPosition, pathToTarget);
    }
}