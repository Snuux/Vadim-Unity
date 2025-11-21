using UnityEngine;

public class SimpleDraggable : MonoBehaviour
{
    private const float MinTorque = -.5f;
    private const float MaxTorque = .5f;
    private const float DefaultRayDistance = 100f;
    private const float Treshhold = 0.1f;

    [SerializeField] private float yDragPosition;

    private Rigidbody _rigidbody;
    private bool _isDragging;

    private void Awake()
    {
        if (_rigidbody == null)
            return;

        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_isDragging == true)
            OnDrag();
    }

    public void Initialize(Rigidbody rigidbody)
    {
        _rigidbody = rigidbody;
    }

    public void BeginDrag()
    {
        _isDragging = true;
    }

    public void EndDrag()
    {
        _rigidbody.AddTorque(Vector3.forward * Random.Range(MinTorque, MaxTorque), ForceMode.Impulse);
        _rigidbody.AddTorque(Vector3.right * Random.Range(MinTorque, MaxTorque), ForceMode.Impulse);

        _isDragging = false;
        Destroy(gameObject);
    }

    private void OnDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, DefaultRayDistance)
        );

        Vector3 _hitPoint = Vector3.zero;

        if (Physics.Raycast(ray, out RaycastHit hit, DefaultRayDistance))
            _hitPoint = hit.point;

        Vector3 direction = (mouseWorldPos - _hitPoint).normalized;

        if (direction.magnitude > Treshhold)
        {
            _rigidbody.isKinematic = true;

            float t = (yDragPosition - ray.origin.y) / ray.direction.y;
            _rigidbody.position = ray.origin + ray.direction * t;

            _rigidbody.isKinematic = false;
        }
    }
}