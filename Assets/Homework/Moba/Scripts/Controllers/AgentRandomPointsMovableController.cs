using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class AgentRandomPointsMovableController : Controller
{
    private const float Threshold = 0.1f;

    private IAgentMovable _agent;

    private NavMeshPath _pathToTarget;
    List<MovePointCharacter> _pointsToMove;

    public AgentRandomPointsMovableController(IAgentMovable agent, List<MovePointCharacter> points)
    {
        _pointsToMove = points;
        _agent = agent;
        _pathToTarget = new NavMeshPath();
    }

    public override void UpdateControlling(float deltaTime)
    {
        MovePointCharacter pointCharacter = GetRandomPoint();

        if (pointCharacter != null)
            _agent.SetDestination(pointCharacter.Position);
    }

    private MovePointCharacter GetRandomPoint()
    {
        List<MovePointCharacter> availiblePoints = new List<MovePointCharacter>();

        foreach (var point in _pointsToMove)
        {
            if ((point.Position - _agent.Position).magnitude <= point.Radius &&
                (point.Position - _agent.Position).magnitude >= Threshold &&
                _agent.TryGetPath(point.Position, _pathToTarget))
                availiblePoints.Add(point);
        }

        if (availiblePoints.Count == 0)
            return null;

        return availiblePoints[Random.Range(0, availiblePoints.Count)];
    }
}