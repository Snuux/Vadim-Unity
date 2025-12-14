using System.Collections;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _radius;
    [SerializeField] private float _secondsToExplode;

    [SerializeField] private Spawner _explosionPrefab;

    private Coroutine _explosionCoroutine;
    private bool _isExploded;

    public bool IsExploded => _isExploded;

    private void Update()
    {
        if (IsExplodableInsideRadius() && _explosionCoroutine == null)
            _explosionCoroutine = StartCoroutine(StartTickingBeforeExplode());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    private IEnumerator StartTickingBeforeExplode()
    {
        float timer = 0;
        while (timer < _secondsToExplode)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        Explode();

        if (_explosionPrefab != null)
            _explosionPrefab.Spawn(transform.position);

        _explosionCoroutine = null;

        _isExploded = true;
        yield return null;
        _isExploded = false;
        yield return null;

        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, _radius);

        foreach (Collider target in targets)
            if (target.TryGetComponent(out IExplodable explodable))
            {
                explodable.TakeExplosion();

                if (target.TryGetComponent(out IDamagable damagable))
                    damagable.TakeDamage(_damage);
            }
    }


    private bool IsExplodableInsideRadius()
    {
        bool insideRadius = false;

        Collider[] targets = Physics.OverlapSphere(transform.position, _radius);

        foreach (Collider target in targets)
            if (target.TryGetComponent(out IExplodable explodable))
                insideRadius = true;

        return insideRadius;
    }
}