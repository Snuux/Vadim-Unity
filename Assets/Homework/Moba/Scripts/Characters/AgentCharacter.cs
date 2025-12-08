using UnityEngine;
using UnityEngine.AI;

public class AgentCharacter : MonoBehaviour, IAgentMovable, IAgentRotatable, IExplodable, IDamagable
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _maxHealth;

    [SerializeField] private float _requiredTimeToLongIdle;

    private AgentMover _mover;
    private TransformRotator _rotator;
    private HealthComponent _healthStat;

    private Vector3 _defaultPosition;
    private NavMeshAgent _agent;

    public Vector3 CurrentVelocity => _agent.desiredVelocity;
    public Vector3 Position => transform.position;
    public Vector3 CurrentDestination => _mover.CurrentDestination;

    public float CurrentHealth => _healthStat.CurrentValue;
    public float MaxHealth => _healthStat.MaxValue;
    public bool IsDeadTrigger => _healthStat.IsDeadTrigger;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _rotator = new TransformRotator(transform, _rotationSpeed);
        _healthStat = new HealthComponent(_maxHealth);
        _mover = new AgentMover(_agent, _moveSpeed, _healthStat, _requiredTimeToLongIdle);

        _defaultPosition = transform.position;
    }

    private void Update()
    {
        _healthStat.Update(Time.deltaTime);
        _mover.Update(Time.deltaTime);
        _rotator.Update(Time.deltaTime);
    }

    private void LateUpdate()
    {
        _healthStat.LateUpdate(Time.deltaTime);
    }

    public void StopMove() => _mover.Stop();

    public void ResumeMove() => _mover.Resume();

    public bool TryGetPath(Vector3 targetPosition, NavMeshPath pathToTarget)
        => NavMeshUtils.TryGetPath(_agent, targetPosition, pathToTarget);

    public void SetDestination(Vector3 position) => _agent.SetDestination(position);

    public void SetRotationDirection(Vector3 direction) => _rotator.SetRotation(direction);

    public void Teleport(Vector3 position) => _mover.Teleport(position);

    public void TeleportToDefaultPosition() => Teleport(_defaultPosition);

    public void TakeDamage(float damage) => _healthStat.TakeDamage(damage);

    public void TakeExplosion() { }

    public bool IsInjured() => _healthStat.IsInjured();
    
    public bool IsDead() => _healthStat.IsDead();

    public void Revive() => _healthStat.Revive();

    public void Die() => _agent.ResetPath();

    public bool IsLongIdle() => _mover.IsLongIdle();

    public bool IsStaying() => _mover.IsStaying();

    public bool HasPath() => _mover.HasPath();
}
