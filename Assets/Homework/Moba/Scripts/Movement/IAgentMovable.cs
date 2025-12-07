using UnityEngine;
using UnityEngine.AI;

public interface IAgentMovable : ITransformPosition
{
    Vector3 CurrentVelocity { get; }
    bool IsLongIdle { get; }
    bool HasPath { get; }

    void SetDestination(Vector3 direction);
    void SpawnAnchorPoint(Vector3 position);
    bool TryGetPath(Vector3 targetPosition, NavMeshPath pathToTarget);
}
