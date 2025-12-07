using UnityEngine;

class ExplosionController : Controller
{
    private IExplosionSource _explosionSource;

    private float _activateRadius;
    private float _secondsToExplode;

    private float _secondsBeforeExplode;
    private bool _startExplodeTimer;

    public ExplosionController(IExplosionSource explosionSource, float activateRadius, float secondsToExplode)
    {
        _explosionSource = explosionSource;
        _activateRadius = activateRadius;
        _secondsToExplode = secondsToExplode;
    }

    public override void UpdateControlling(float deltaTime)
    {
        if (IsExplodableInsideRadius(_explosionSource.Position, _activateRadius))
            _startExplodeTimer = true;

        if (_startExplodeTimer)
            _secondsBeforeExplode += Time.deltaTime;

        if (_secondsBeforeExplode >= _secondsToExplode)
        {
            _startExplodeTimer = false;
            _secondsBeforeExplode = 0;

            _explosionSource.Explode(_explosionSource.Position);
        }
    }

    public bool IsExplodableInsideRadius(Vector3 position, float activateRadius)
    {
        bool insideRadius = false;

        Collider[] targets = Physics.OverlapSphere(position, activateRadius);

        foreach (Collider target in targets)
            if (target.TryGetComponent(out IDamageExplodable explodable))
                insideRadius = true;

        return insideRadius;
    }
}