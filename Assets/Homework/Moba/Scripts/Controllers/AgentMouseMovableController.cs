using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class AgentMouseMovableController : Controller
{
    private const float DefaultRayDistance = 1000f;
    private const float Treshhold = 0.1f;
    private const float PeriodBetweenMouseButtonClicks = .4f;

    private IAgentMovable _agent;
    private Vector3 _destinationPosition;
    private float _timeBetweenMouseButtonClicks;

    private NavMeshPath _pathToTarget;
    private bool _isValidPath;

    public AgentMouseMovableController(IAgentMovable agent)
    {
        _agent = agent;
        _pathToTarget = new NavMeshPath();
    }

    public override void UpdateControlling(float deltaTime)
    {
        _timeBetweenMouseButtonClicks += Time.deltaTime;

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButton(0) && _timeBetweenMouseButtonClicks >= PeriodBetweenMouseButtonClicks)
        {
            ProcessDestinationPosition();
            _timeBetweenMouseButtonClicks = 0;
        }
        else if (Input.GetMouseButtonDown(0)) //когда мы быстро тыкаем по мышке, вместа зажатия
        {
            ProcessDestinationPosition();
        }
    }

    private void ProcessDestinationPosition()
    {
        Ray ray = ScreenPointToRay();
        Vector3 mouseWorldPos = MouseWorldPosition();

        _destinationPosition = GetHitPoint(ray);

        _isValidPath = _agent.TryGetPath(_destinationPosition, _pathToTarget);

        if (_isValidPath)
        {
            if (_destinationPosition.magnitude > Treshhold)
                _agent.SetDestination(_destinationPosition);
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
