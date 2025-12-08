using UnityEngine;

public class AnchorPointSpawnView : MonoBehaviour
{
    private const float Threshold = .01f;
    private const float AnchorPointParticleSystemOffsetY = .5f;

    [SerializeField] private AgentCharacter _character;
    [SerializeField] private Spawner _anchorPointPrefab;

    private Vector3 _previousDestination;
    private bool _isChangedDestination;

    private void Update()
    {
        ComparePreviousAndCurrentDestination();
        SpawnAnchorPoint();
    }

    private void LateUpdate()
    {
        ResetIsChangedDestination();
    }

    private void ResetIsChangedDestination() => _isChangedDestination = false;

    private void SpawnAnchorPoint()
    {
        if (_isChangedDestination && Input.GetMouseButton(0))
            _anchorPointPrefab.Spawn(_character.CurrentDestination + AnchorPointParticleSystemOffsetY * Vector3.up);
    }

    private void ComparePreviousAndCurrentDestination()
    {
        float distanceBetweenPreviousAndCurrent = (_previousDestination - _character.CurrentDestination).magnitude;

        if (distanceBetweenPreviousAndCurrent >= Threshold)
            _isChangedDestination = true;

        _previousDestination = _character.CurrentDestination;
    }
}