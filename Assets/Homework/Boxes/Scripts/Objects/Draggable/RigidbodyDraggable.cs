using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyDraggable : MonoBehaviour, IDraggable
{
    private const float MinTorque = -.5f;
    private const float MaxTorque = .5f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnGrab()
    {
    }

    public void OnDrag(Vector3 targetPosition)
    {
        _rigidbody.isKinematic = true;
        _rigidbody.position = targetPosition;
        _rigidbody.isKinematic = false;
    }

    public void OnRelease()
    {
        _rigidbody.AddTorque(Vector3.forward * Random.Range(MinTorque, MaxTorque), ForceMode.Impulse);
        _rigidbody.AddTorque(Vector3.right * Random.Range(MinTorque, MaxTorque), ForceMode.Impulse);
    }
}