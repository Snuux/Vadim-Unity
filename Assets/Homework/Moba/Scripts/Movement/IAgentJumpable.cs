using UnityEngine;
using UnityEngine.AI;

public interface IAgentJumpable : ITransformPosition
{
    bool InJumpProcess { get; }

    bool IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData);

    public void Jump(OffMeshLinkData offMeshLinkData);
}