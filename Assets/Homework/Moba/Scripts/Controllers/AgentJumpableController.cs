using UnityEngine;
using UnityEngine.AI;

public class AgentJumpableController : Controller
{
    private IAgentJumpable _agentJumpable;
    private IDirectionRotatable _directionRotatable;

    public AgentJumpableController(
        IAgentJumpable agentJumpable, 
        IDirectionRotatable directionRotatable)
    {
        _agentJumpable = agentJumpable;
        _directionRotatable = directionRotatable;
    }

    public override void UpdateLogic(float deltaTime)
    {
        if (_agentJumpable.IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData))
        {
            if (_agentJumpable.InJumpProcess == false)
                _agentJumpable.Jump(offMeshLinkData);

            if (_agentJumpable.InJumpProcess)
            {
                Vector3 direction = offMeshLinkData.endPos - _agentJumpable.Position;
                direction.y = 0f;
                _directionRotatable.SetRotationDirection(direction);
            }    
        }
    }

    public bool InJumpProcess()
    {
        return _agentJumpable.InJumpProcess;
    }
}