using Boxes;
using UnityEngine;
using UnityEngine.AI;

public class AgentCharacter : MonoBehaviour, IAgentMovable, IAgentRotatable, IDamageExplodable, IDamagable
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _maxHealth;

    [SerializeField] private float _requiredTimeToLongIdle;

    [SerializeField] private ParticleSystem _anchorPointParticleSystemPrefab;
    [SerializeField] private float _anchorPointParticleSystemOffsetY;
    [SerializeField] private ParticleSystem _diePointParticleSystemPrefab;
    [SerializeField] private float _diePointParticleSystemOffsetY;

    private AgentMover _mover;
    private TransformRotator _rotator;
    private HealthStat _healthStat;

    private float _currentTimeToLongIdle;

    private Vector3 _defaultPosition;
    private NavMeshAgent _agent;

    public Vector3 CurrentVelocity => _agent.desiredVelocity;
    public Vector3 Position => transform.position;

    public float CurrentHealth => _healthStat.CurrentValue;
    public float MaxHealth => _healthStat.MaxValue;

    public NavMeshAgent NavAgent => _agent;

    public bool IsLongIdle => _currentTimeToLongIdle >= _requiredTimeToLongIdle;
    public bool HasPath => _agent.hasPath;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _mover = new AgentMover(_agent, _moveSpeed);
        _rotator = new TransformRotator(transform, _rotationSpeed);
        _healthStat = new HealthStat(_maxHealth);

        _defaultPosition = transform.position;
    }

    private void Update()
    {
        if (IsDead())
            return;

        _mover.Update(Time.deltaTime);
        _rotator.Update(Time.deltaTime);

        if (IsInjured())
            _mover.SetMoveSpeed(_moveSpeed * HealthStat.InjuredMoveSpeedModifier);
        else
            _mover.SetMoveSpeed(_moveSpeed);

        if (HasPath == false)
            _currentTimeToLongIdle += Time.deltaTime;
        else
            _currentTimeToLongIdle = 0;
    }

    public void StopMove() => _mover.Stop();

    public void ResumeMove() => _mover.Resume();

    public void SpawnAnchorPoint(Vector3 position) =>
        Instantiate(_anchorPointParticleSystemPrefab, 
            position + _anchorPointParticleSystemOffsetY * Vector3.up, 
            Quaternion.identity);

    public bool TryGetPath(Vector3 targetPosition, NavMeshPath pathToTarget)
        => NavMeshUtils.TryGetPath(_agent, targetPosition, pathToTarget);

    public void SetDestination(Vector3 position) => _agent.SetDestination(position);

    public void SetRotationDirection(Vector3 direction) => _rotator.SetRotation(direction);

    public void Teleport(Vector3 position) => _mover.Teleport(position);

    public void TakeDamage(float damage)
    {
        //запоминаем кол-во хп, наносим дамаг, если хп было >0, то умираем.
        //Если нет, то уже умерли, и не нужно вызывать Die()

        float healthBeforeDamage = _healthStat.CurrentValue;

        _healthStat.TakeDamage(damage);

        if (IsDead() && healthBeforeDamage > 0)
            Die();
    }

    public bool IsInjured() => _healthStat.IsInjured();
    
    public bool IsDead() => _healthStat.IsDead();

    public void Revive() => _healthStat.Revive();

    public void ReviveAndTeleportToDefaultPosition()
    {
        Revive();
        Teleport(_defaultPosition);
    }

    public void Die()
    {
        Instantiate(_diePointParticleSystemPrefab, 
            transform.position + _diePointParticleSystemOffsetY * Vector3.up, 
            Quaternion.identity);
        _agent.ResetPath();
    }

    public void TakeExplosion(float damage)
    {
        TakeDamage(damage);
    }
}
