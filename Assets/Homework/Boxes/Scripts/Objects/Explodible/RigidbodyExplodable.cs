using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class RigidbodyExplodable : MonoBehaviour, IExplodable
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
     
    public void Explode(Vector3 point, Vector3 force)
    {
        _rigidbody.AddForce(force, ForceMode.Impulse);
    }
}