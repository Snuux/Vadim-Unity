using UnityEngine;

public class ExplosionEffect : IShootEffect
{
    private float _force;
    private float _radius;
    private Spawner _fxSpawner;
    private LayerMask _layerMask;

    public ExplosionEffect(float force, float radius, Spawner fxSpawnerPrefab, LayerMask layerMask)
    {
        _force = force;
        _radius = radius;
        _fxSpawner = fxSpawnerPrefab;
        _layerMask = layerMask;
    }

    public void Execute(Vector3 point)
    {
        Collider[] targets = Physics.OverlapSphere(point, _radius);

        foreach (Collider target in targets)
        {
            if (target.TryGetComponent(out Rigidbody rigidbody))
            {
                Vector3 direction = (rigidbody.position - point).normalized;
                rigidbody.AddForce(direction * _force, ForceMode.Impulse);
            }
        }

        _fxSpawner.Spawn(point);
    }

    public void StopExecute()
    {

    }
}
