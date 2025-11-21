using UnityEngine;

class Player : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    [SerializeField] private bool _debugGizmosVisible;

    [SerializeField] private Spawner fxSpawner;
    [SerializeField] private Spawner simpleDraggable;

    [SerializeField] private LayerMask _shooterMask;
    [SerializeField] private LayerMask _dragMask;

    private IShooter shooter;
    private IShooter drag;

    private void Awake()
    {
        shooter = new RayShooter(new ExplosionEffect(_explosionForce, _explosionRadius, fxSpawner, _shooterMask));
        drag = new RayShooter(new DragEffect(simpleDraggable, _dragMask));
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(1))
        {
            shooter.Shoot(ray.origin, ray.direction);
            shooter.StopShoot(ray.origin, ray.direction);
        }

        if (Input.GetMouseButtonDown(0))
            drag.Shoot(ray.origin, ray.direction);

        if (Input.GetMouseButtonUp(0))
            drag.StopShoot(ray.origin, ray.direction);
    }

    private void OnDrawGizmos()
    {
        if (_debugGizmosVisible)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)
            );

            float t = (1f - ray.origin.y) / ray.direction.y;
            Vector3 position = ray.origin + ray.direction * t;

            Gizmos.color = Color.black;
            Gizmos.DrawSphere(position, 0.2f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(position, _explosionRadius);
        }
    }
}
