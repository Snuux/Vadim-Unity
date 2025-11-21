using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            Debug.Log(hit.collider.gameObject.name);
        }

        if (IsInRange(transform, target, 45))
        {
            target.transform.localScale = new Vector3(2,2,2);

            Debug.Log("InRange");
        }
        else
        {
            target.transform.localScale = new Vector3(1, 1, 1);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 100f);
    }

    private bool IsInRange(Transform source, Transform target, float angle)
    {
        Vector3 direction = target.position - source.position;
        float angleToTarget = AngleToTarget(source, direction);

        if (angleToTarget > angle / 2)
            return false;
        return true;
    }

    private float AngleToTarget(Transform source, Vector3 direction)
    {
        float dotProduct = Vector3.Dot(direction, source.forward);
        float cos = dotProduct / (direction.magnitude * source.forward.magnitude);
        float angleToTarget = Mathf.Acos(cos) * Mathf.Rad2Deg;

        return angleToTarget;
    }
}
