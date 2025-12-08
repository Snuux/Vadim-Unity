using Boxes;
using UnityEngine;

class ExplosionSourceExplodeController : Controller
{
    private IExplosionSource _explosionSource;

    private float _radius;
    private float _secondsToExplode;

    private float _secondsBeforeExplode;
    private bool _startExplodeTimer;

    public ExplosionSourceExplodeController(IExplosionSource explosionSource, float radius, float secondsToExplode)
    {
        _explosionSource = explosionSource;
        _radius = radius;
        _secondsToExplode = secondsToExplode;
    }

    public override void UpdateControlling(float deltaTime)
    {
        if (IsExplodableInsideRadius(_explosionSource.Position, _radius))
            _startExplodeTimer = true;

        if (_startExplodeTimer)
            _secondsBeforeExplode += Time.deltaTime;

        if (_secondsBeforeExplode >= _secondsToExplode)
        {
            _startExplodeTimer = false;
            _secondsBeforeExplode = 0;

            _explosionSource.Explode();
        }
    }

    public bool IsExplodableInsideRadius(Vector3 position, float activateRadius)
    {
        bool insideRadius = false;

        Collider[] targets = Physics.OverlapSphere(position, activateRadius);

        foreach (Collider target in targets)
            if (target.TryGetComponent(out IExplodable explodable))
                insideRadius = true;

        return insideRadius;
    }
}