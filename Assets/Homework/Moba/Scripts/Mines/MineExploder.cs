using UnityEngine;

public class MineExploder
{
    private float _activateRadius;

    public MineExploder(float activateRadius)
    {
        _activateRadius = activateRadius;
    }

    public void Update(float deltaTime)
    {
    }

    public void Explode(Vector3 point, float damage)
    {
        Collider[] targets = Physics.OverlapSphere(point, _activateRadius);

        foreach (Collider target in targets)
            if (target.TryGetComponent(out IDamageExplodable explodable))
                explodable.TakeExplosion(damage);
    }
}