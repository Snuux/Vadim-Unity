using UnityEngine;

public class MineCharacter : MonoBehaviour, IExplosionSource
{
    [SerializeField] private float _damage;

    [SerializeField] private float _radius;
    [SerializeField] private float _secondsToExplode;

    [SerializeField] private Spawner _explosionPrefab;

    private ExplosionSourceExploder _mineExploder;
    private bool _isExploded;

    public Vector3 Position => transform.position;

    public float Damage => _damage;
    public float Radius => _radius;
    public float SecondsToExplode => _secondsToExplode;

    public bool IsExploded => _isExploded;

    public void Initialize(float damage, float radius, float secondsToExplode)
    {
        _damage = damage;
        _radius = radius;
        _secondsToExplode = secondsToExplode;
    }

    private void Awake()
    {
        _mineExploder = new ExplosionSourceExploder(_radius, _damage);
    }

    private void Update()
    {
        _mineExploder.Update(Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    public void Explode()
    {
        _mineExploder.Explode(transform.position);
        _isExploded = true;

        _explosionPrefab.Spawn(transform.position);
        Destroy(gameObject);
    }
}