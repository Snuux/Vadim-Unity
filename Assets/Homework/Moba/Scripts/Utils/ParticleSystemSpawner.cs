using UnityEngine;

public class ParticleSystemSpawner : Spawner
{
    [SerializeField] ParticleSystem _particleSystemPrefab;

    public void Initialize(ParticleSystem particleSystemPrefab)
    {
        _particleSystemPrefab = particleSystemPrefab;
    }

    public override void Spawn(Vector3 position)
    {
        Instantiate(_particleSystemPrefab, position, Quaternion.identity);
    }
}