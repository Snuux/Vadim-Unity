using MobaLesson;
using UnityEngine;
using UnityEngine.AI;

public class AgentCharacter : MonoBehaviour, IAgentMovable, IAgentJumpable, IDirectionRotatable, IExplodable, IDamagable
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _requiredTimeToLongIdle;
    [SerializeField] private AnimationCurve _jumpCurve;

    [SerializeField] private float _injuredHealthMultiplier;
    [SerializeField] private float _injuredSpeedMultiplier;

    private AgentMover _mover;
    private AgentJumper _jumper;
    private TransformRotator _rotator;
    private HealthComponent _health;

    private Vector3 _defaultPosition;
    private NavMeshAgent _agent;

    public Vector3 CurrentVelocity => _agent.desiredVelocity;
    public Vector3 Position => transform.position;
    public Quaternion Rotation => transform.rotation;
    public Vector3 CurrentDestination => _mover.CurrentDestination;

    public float CurrentHealth => _health.CurrentValue;
    public float MaxHealth => _health.MaxValue;
    public bool IsDeadTrigger => _health.IsDeadTrigger;
    public bool InJumpProcess => _jumper.InProcess;
    public float RequiredTimeToLongIdle => _requiredTimeToLongIdle;
    public AnimationCurve JumpCurve => _jumpCurve;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _rotator = new TransformRotator(transform);
        _health = new HealthComponent(_maxHealth, _injuredHealthMultiplier);
        _mover = new AgentMover(_agent, _moveSpeed);
        _jumper = new AgentJumper(_jumpSpeed, _agent, this, _jumpCurve);

        _defaultPosition = transform.position;
    }

    private void Update()
    {
        _health.Update(Time.deltaTime);

        if (InJumpProcess == false)
            _rotator.Update(Time.deltaTime);

        _mover.Update(Time.deltaTime);

        UpdateMoveSpeedByHealth(_injuredSpeedMultiplier);
    }

    private void LateUpdate()
    {
        _health.LateUpdate(Time.deltaTime);
    }

    public void StopMove() => _mover.Stop();

    public void ResumeMove() => _mover.Resume();

    public bool TryGetPath(Vector3 targetPosition, NavMeshPath pathToTarget)
        => NavMeshUtils.TryGetPath(_agent, targetPosition, pathToTarget);

    public void SetDestination(Vector3 position) => _mover.SetDestination(position);

    public void SetRotationDirection(Vector3 direction) => _rotator.SetRotation(direction, _rotationSpeed);

    public void Teleport(Vector3 position) => _mover.Teleport(position);

    public void TeleportToDefaultPosition() => Teleport(_defaultPosition);

    public void TakeDamage(float damage) => _health.TakeDamage(damage);

    public void TakeExplosion() { }

    public bool IsInjured() => _health.IsInjured();

    public bool IsDead() => _health.IsDead();

    public void Revive() => _health.Revive();

    public void Die() => _agent.ResetPath();

    public bool IsStaying() => _mover.IsStaying();

    public bool HasPath() => _mover.HasPath();

    public void SetMovementSpeed(float speed) => _mover.SetMovementSpeed(speed);

    public float GetMovementSpeed() => _mover.GetMovementSpeed();

    public float GetDefaultMovementSpeed() => _mover.GetDefaultMovementSpeed();

    private void UpdateMoveSpeedByHealth(float speedMultiplier)
    {
        float defaultAgentMovementSpeed = GetDefaultMovementSpeed();

        if (IsInjured())
            SetMovementSpeed(defaultAgentMovementSpeed * speedMultiplier);
        else
            SetMovementSpeed(defaultAgentMovementSpeed);
    }

    public bool IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData)
    {
        if (_agent.isOnOffMeshLink)
        {
            offMeshLinkData = _agent.currentOffMeshLinkData;
            return true;
        }
    
        offMeshLinkData = default(OffMeshLinkData);
        return false;
    }

    public void Jump(OffMeshLinkData offMeshLinkData)
    {
         _jumper.Jump(offMeshLinkData);
    }

}
