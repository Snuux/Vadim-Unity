using UnityEngine;

public class ExplosionSourceExploder
{
    private float _radius;
    private float _damage;

    public ExplosionSourceExploder(float explosionRadius, float damage)
    {
        _radius = explosionRadius;
        _damage = damage;
    }

    public void Update(float deltaTime)
    {
    }

    public void Explode(Vector3 point)
    {
        Collider[] targets = Physics.OverlapSphere(point, _radius);

        foreach (Collider target in targets)
            if (target.TryGetComponent(out IExplodable explodable))
            {
                explodable.TakeExplosion();

                if (target.TryGetComponent(out IDamagable damagable))
                    damagable.TakeDamage(_damage);
            }    
    }
}