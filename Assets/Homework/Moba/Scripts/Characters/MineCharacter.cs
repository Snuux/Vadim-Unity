using UnityEngine;

public class MineCharacter : MonoBehaviour, IExplosionSource
{
    [SerializeField] private float _damage;

    [SerializeField] float _explosionRadius;
    [SerializeField] float _activateRadius;
    [SerializeField] float _secondsToExplode;

    [SerializeField] private ParticleSystem _explosionParticleSystemPrefab;

    private MineExploder _mineExploder;
    private bool _isExploded;

    public Vector3 Position => transform.position;

    public float Damage => _damage;
    public bool IsExploded => _isExploded;

    public float ExplosionRadius => _explosionRadius;
    public float SecondsToExplode => _secondsToExplode;

    public float Radius => _explosionRadius;
    public float ActivateRadius => _activateRadius;

    private void Awake()
    {
        _mineExploder = new MineExploder(_activateRadius);
    }

    private void Update()
    {
        _mineExploder.Update(Time.deltaTime);
    }

    public void Explode(Vector3 point)
    {
        _mineExploder.Explode(point, Damage);
        _isExploded = true;

        Instantiate(_explosionParticleSystemPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(Position, Radius);
    }
}