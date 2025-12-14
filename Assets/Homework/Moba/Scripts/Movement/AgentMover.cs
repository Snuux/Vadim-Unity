using UnityEngine;
using UnityEngine.AI;

public class AgentMover
{
    private float StayThreshold = .001f;

    private NavMeshAgent _agent;
    private NavMeshPath _path;

    private float _defaultMovementSpeed;

    public Vector3 CurrentVelocity => _agent.desiredVelocity;
    public Vector3 CurrentDestination => _agent.destination;

    public AgentMover(NavMeshAgent agent, float defaultMovementSpeed)
    {
        _agent = agent;
        _agent.speed = defaultMovementSpeed;
        _defaultMovementSpeed = defaultMovementSpeed;

        _agent.acceleration = 999;
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _path = new NavMeshPath();
    }

    public void Update(float deltaTime)
    {
    }

    public void SetMovementSpeed(float moveSpeed) => _agent.speed = moveSpeed;

    public float GetMovementSpeed() => _agent.speed;

    public float GetDefaultMovementSpeed() => _defaultMovementSpeed;

    public void SetDestination(Vector3 position)
    {
        _agent.SetDestination(position);

        //int originalMask = _agent.areaMask;
        //int offLinkArea = NavMesh.GetAreaFromName("Jump");
        //
        //int maskWithoutLinks = originalMask & ~(0001 << offLinkArea);
        //
        //_agent.areaMask = maskWithoutLinks;
        //
        //bool canReachWithoutLinks =
        //    NavMesh.CalculatePath(_agent.transform.position, position, maskWithoutLinks, _path) &&
        //    _path.status == NavMeshPathStatus.PathComplete;
        //
        //if (canReachWithoutLinks)
        //    _agent.SetPath(_path);
        //else
        //    _agent.SetDestination(position);
        //
        //_agent.areaMask = originalMask;


    }

    public void Stop() => _agent.isStopped = true;

    public void Resume() => _agent.isStopped = false;

    public void Teleport(Vector3 position)
    {
        _agent.ResetPath();
        _agent.Warp(position);
    }

    public bool HasPath() => _agent.hasPath;

    public bool IsStaying() => CurrentVelocity.magnitude <= StayThreshold;
}
