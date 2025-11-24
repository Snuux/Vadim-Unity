using UnityEngine;

public interface IDraggable
{
    void OnGrab();
    void OnDrag(Vector3 targetPosition);
    void OnRelease();
}
