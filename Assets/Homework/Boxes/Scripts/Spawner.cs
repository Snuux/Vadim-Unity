using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject _gameObject;

    public GameObject Spawn(Vector3 position)
    {
        return Instantiate(_gameObject, position, Quaternion.identity);
    }
}