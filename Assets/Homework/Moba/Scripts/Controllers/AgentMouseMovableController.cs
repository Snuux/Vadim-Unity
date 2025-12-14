using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class AgentMouseMovableController : Controller
{
    private const float PeriodBetweenMouseButtonClicks = .1f;
    private const float DefaultRayDistance = 1000f;
    private const float Treshhold = 0.1f;

    private IAgentMovable _agentMovable;
    private Vector3 _destinationPosition;

    private NavMeshPath _pathToTarget;
    private bool _isValidPath;

    private MouseContinousClicksChecker _continousClicks;

    public AgentMouseMovableController(IAgentMovable agentMovable)
    {
        _agentMovable = agentMovable;

        _pathToTarget = new NavMeshPath();
        _continousClicks = new MouseContinousClicksChecker();
    }

    public override void UpdateLogic(float deltaTime)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (_continousClicks.IsMouseButtonPressEverySecond(PeriodBetweenMouseButtonClicks))
            ProcessDestinationPosition();
    }

    private void ProcessDestinationPosition()
    {
        Ray ray = ScreenPointToRay();
        Vector3 mouseWorldPos = MouseWorldPosition();

        _destinationPosition = GetHitPoint(ray);
        _isValidPath = _agentMovable.TryGetPath(_destinationPosition, _pathToTarget);

        if (_isValidPath)
        {
            if (_destinationPosition.magnitude > Treshhold)
                _agentMovable.SetDestination(_destinationPosition);
        }
    }

    private static Vector3 GetHitPoint(Ray ray)
    {
        Vector3 hitPoint = Vector3.zero;

        if (Physics.Raycast(ray, out RaycastHit hit, DefaultRayDistance))
            hitPoint = hit.point;
        return hitPoint;
    }

    private Ray ScreenPointToRay() => Camera.main.ScreenPointToRay(Input.mousePosition);

    private Vector3 MouseWorldPosition()
        => Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, DefaultRayDistance)
        );
}
