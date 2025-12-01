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
        _rigidbody.isKinematic = true;
    }

    public void OnDrag(Vector3 targetPosition)
    {
        _rigidbody.position = targetPosition;
    }

    public void OnRelease()
    {
        _rigidbody.isKinematic = false;

        _rigidbody.AddTorque(Vector3.forward * Random.Range(MinTorque, MaxTorque), ForceMode.Impulse);
        _rigidbody.AddTorque(Vector3.right * Random.Range(MinTorque, MaxTorque), ForceMode.Impulse);
    }
}