using UnityEngine;
using UnityEngine.AI;

public class PlayerAgent : MonoBehaviour
{
    private const float DefaultRayDistance = 1000f;
    private const float Treshhold = 0.1f;

    [SerializeField] private float _movementSpeed;

    private NavMeshAgent _agent;

    private Vector3 _destinationPosition;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _movementSpeed;
        _agent.acceleration = 999;

        _agent.updateRotation = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = ScreenPointToRay();
            Vector3 mouseWorldPos = MouseWorldPosition();

            _destinationPosition = GetHitPoint(ray);
        }

        if (_destinationPosition.magnitude > Treshhold)
            _agent.SetDestination(_destinationPosition);
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
