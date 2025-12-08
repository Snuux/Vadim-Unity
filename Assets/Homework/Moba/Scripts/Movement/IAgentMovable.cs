using UnityEngine;
using UnityEngine.AI;

public interface IAgentMovable : ITransformPosition
{
    Vector3 CurrentVelocity { get; }

    void SetDestination(Vector3 direction);
    bool TryGetPath(Vector3 targetPosition, NavMeshPath pathToTarget);
}
