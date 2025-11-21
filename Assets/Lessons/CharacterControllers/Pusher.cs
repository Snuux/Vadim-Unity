using UnityEngine;

public class Pusher : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 50, ForceMode.Impulse);
    }
}
