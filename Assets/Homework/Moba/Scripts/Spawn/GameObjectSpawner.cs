using UnityEngine;

public class GameObjectSpawner : Spawner
{
    [SerializeField] GameObject _gameObject;

    public void Initialize(GameObject particleSystemPrefab)
    {
        _gameObject = particleSystemPrefab;
    }

    public override void Spawn(Vector3 position)
    {
        if (_gameObject != null)
            Instantiate(_gameObject, position, Quaternion.identity);
    }
}
