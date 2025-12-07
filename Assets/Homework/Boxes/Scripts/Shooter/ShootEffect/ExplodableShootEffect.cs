using UnityEngine;

namespace Boxes
{
    public class ExplodableShootEffect : IShootEffect
    {
        private float _force;
        private float _radius;

        private ParticleSystemSpawner _fxSpawner;

        public ExplodableShootEffect(float force, float radius, ParticleSystemSpawner fxSpawnerPrefab)
        {
            _force = force;
            _radius = radius;

            _fxSpawner = fxSpawnerPrefab;
        }

        public void Execute(Vector3 point)
        {
            Collider[] targets = Physics.OverlapSphere(point, _radius);

            foreach (Collider target in targets)
            {
                if (target.TryGetComponent(out IExplodable explodable))
                {
                    Vector3 direction = (target.transform.position - point).normalized;
                    explodable.Explode(point, direction * _force);
                }
            }

            _fxSpawner.Spawn(point);
        }
    }
}