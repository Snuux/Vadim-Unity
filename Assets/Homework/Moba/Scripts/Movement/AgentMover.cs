using UnityEngine;
using UnityEngine.AI;

public class AgentMover
{
    private float StayThreshold = .001f;

    private NavMeshAgent _agent;
    private HealthComponent _healthComponent;

    private float _requiredTimeToLongIdle;
    private float _currentTimeToLongIdle;

    private float _movementSpeed;

    public Vector3 CurrentVelocity => _agent.desiredVelocity;
    public Vector3 CurrentDestination => _agent.destination;

    public AgentMover(NavMeshAgent agent, float movementSpeed, HealthComponent healthComponent, float requiredTimeToLongIdle)
    {
        _agent = agent;
        _agent.speed = movementSpeed;
        _movementSpeed = movementSpeed;
        _healthComponent = healthComponent;

        _agent.acceleration = 999;
        _agent.updateRotation = false;
        _requiredTimeToLongIdle = requiredTimeToLongIdle;
    }

    public void Update(float deltaTime)
    {
        UpdateMoveSpeedByHealth();
        UpdateIsLongIdle(Time.deltaTime);
    }

    private void UpdateIsLongIdle(float deltaTime)
    {
        if (HasPath() == false)
            _currentTimeToLongIdle += deltaTime;
        else
            _currentTimeToLongIdle = 0;
    }

    private void UpdateMoveSpeedByHealth()
    {
        if (_healthComponent.IsInjured())
            SetMoveSpeed(_movementSpeed * HealthComponent.InjuredMoveSpeedModifier);
        else
            SetMoveSpeed(_movementSpeed);
    }

    public void SetDestination(Vector3 position) => _agent.SetDestination(position);

    public void Stop() => _agent.isStopped = true;

    public void Resume() => _agent.isStopped = false;

    public void SetMoveSpeed(float moveSpeed) => _agent.speed = moveSpeed;

    public void Teleport(Vector3 position)
    {
        _agent.ResetPath();
        _agent.Warp(position);
    }

    public bool IsLongIdle() => _currentTimeToLongIdle >= _requiredTimeToLongIdle;

    public bool HasPath() => _agent.hasPath;

    public bool IsStaying() => CurrentVelocity.magnitude <= StayThreshold;
}
