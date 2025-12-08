using UnityEngine;

namespace Boxes
{
    class Player : MonoBehaviour
    {
        [SerializeField] private float _explosionForce;
        [SerializeField] private float _explosionRadius;

        [SerializeField] private float _yDragPosition;

        [SerializeField] private bool _debugGizmosVisible;

        [SerializeField] private ParticleSystemSpawner fxSpawner;

        private IShooter _shooter;
        private IDragger _dragger;

        private void Awake()
        {
            _shooter = new RayShooter(new ExplodableShootEffect(_explosionForce, _explosionRadius, fxSpawner));
            _dragger = new MouseDragger(_yDragPosition);
        }

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(1))
                _shooter.Shoot(ray.origin, ray.direction);

            if (Input.GetMouseButtonDown(0))
                _dragger.Start();

            if (Input.GetMouseButtonUp(0))
                _dragger.Stop();
        }

        private void FixedUpdate()
        {
            _dragger.FixedUpdate();
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
}