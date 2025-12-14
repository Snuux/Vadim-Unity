using UnityEngine;
using UnityEngine.AI;

public class NavMeshClickHandler : MonoBehaviour
{
    private const float PeriodBetweenMouseButtonClicks = .1f;
    private const float AnchorPointParticleSystemOffsetY = .5f;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private float navMeshCheckRadius = 0.5f;
    [SerializeField] private Spawner _anchorPointPrefab;

    private MouseContinousClicksChecker _continousClicks;

    private void Awake()
    {
        _continousClicks = new();
    }

    void Update()
    {
        if (_continousClicks.IsMouseButtonPressEverySecond(PeriodBetweenMouseButtonClicks))
        {
            HandleClick();
        }
    }

    private void HandleClick()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (IsPointOnNavMesh(hit.point))
            {
                _anchorPointPrefab.Spawn(hit.point + AnchorPointParticleSystemOffsetY * Vector3.up);
            }
        }
    }

    private bool IsPointOnNavMesh(Vector3 point)
    {
        return NavMesh.SamplePosition(
            point,
            out NavMeshHit navHit,
            navMeshCheckRadius,
            NavMesh.AllAreas
        );
    }
}
