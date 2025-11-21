using UnityEngine;
using UnityEngine.UIElements;

public class BoxTest : MonoBehaviour
{
    [SerializeField] private LayerMask _mask; // Выбирается в инспекторе

    private Ray _ray;
    private Vector3 _mouseWorldPos;
    private Vector3 _hitPoint;

    private Rigidbody _pickedObject;
    private Vector3 _pickedObjectPoint;

    private void Update()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        _mouseWorldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)
        );

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(_ray, out RaycastHit hit, 100f, _mask))
            {
                _hitPoint = hit.point;

                _pickedObject = hit.collider.gameObject.GetComponent<Rigidbody>();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            //float t = (1f - _ray.origin.y) / _ray.direction.y;
            //Vector3 pos = _ray.origin + _ray.direction * t;

            //Vector3 direction = (_pickedObject.position - pos).normalized;

            //direction.y = 0f;


            //float angle = Quaternion.A

            _pickedObject.AddTorque(Vector3.forward * Random.Range(-.5f, .5f), ForceMode.Impulse);
            _pickedObject.AddTorque(Vector3.right * Random.Range(-.5f, .5f), ForceMode.Impulse);

            _pickedObject = null;
        }
    }

    private void FixedUpdate()
    {
        if (_pickedObject != null)
        {
            Vector3 direction = (_mouseWorldPos - _hitPoint).normalized;

            if (direction.magnitude > 0.1f)
            {
                _pickedObject.isKinematic = true;

                float t = (1f - _ray.origin.y) / _ray.direction.y;
                _pickedObjectPoint = _ray.origin + _ray.direction * t;
                _pickedObject.position = _pickedObjectPoint;

                _pickedObject.isKinematic = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(_ray.origin, _ray.direction * 100f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_mouseWorldPos, 0.2f);

        Gizmos.color = Color.black;
        Gizmos.DrawSphere(_hitPoint, 0.2f);
    }
}
