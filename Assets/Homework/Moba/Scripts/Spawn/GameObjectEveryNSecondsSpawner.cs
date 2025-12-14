using Boxes;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.Image;

public class GameObjectEveryNSecondsSpawner : Spawner
{
    private const int _maxSpawnAttempts = 20;

    [SerializeField] private Transform _spawnTarget;
    [SerializeField] private GameObject _healthBoxPrefab;

    [SerializeField] private Vector3 _randomOffset;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _timeBetweenSpawn;

    [SerializeField] private float _spawnCheckRadius;
    [SerializeField] private LayerMask _groundMask;

    private bool _isEnabled = true;

    public bool IsEnabled => _isEnabled;

    private void Awake()
    {
        StartCoroutine(TickingSpawn());
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            _isEnabled = !_isEnabled;
    }

    private IEnumerator TickingSpawn()
    {
        while (true)
        {
            if (_isEnabled == false)
            {
                yield return null;
                continue;
            }

            yield return new WaitForSeconds(_timeBetweenSpawn);

            Vector3 spawnPosition = _spawnTarget.position;
            spawnPosition.x += Random.Range(-_randomOffset.x, _randomOffset.x);
            spawnPosition.z += Random.Range(-_randomOffset.z, _randomOffset.z);
            spawnPosition += _offset;

            Spawn(spawnPosition + _offset);
        }
    }

    public override void Spawn(Vector3 position)
    {
        for (int i = 0; i < _maxSpawnAttempts; i++)
        {
            if (Physics.Raycast(position, Vector3.down, out RaycastHit rayHit, _spawnCheckRadius * 3f,
                _groundMask, QueryTriggerInteraction.Ignore) == false)
                continue;

            if (NavMesh.SamplePosition(position, out NavMeshHit hit, _spawnCheckRadius, NavMesh.AllAreas))
            {
                if (_healthBoxPrefab != null)
                    Instantiate(_healthBoxPrefab, position, Quaternion.identity);
                break;
            }
        }
    }
}