using UnityEngine;

public class MovePointCharacter : MonoBehaviour, ITransformPosition
{
    [field: SerializeField] public float Radius { get; private set; }

    public Vector3 Position => transform.position;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Position, Radius);
    }
}