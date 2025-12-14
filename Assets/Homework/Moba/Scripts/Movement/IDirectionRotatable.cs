using UnityEngine;

public interface IDirectionRotatable : ITransformRotation
{
    void SetRotationDirection(Vector3 direction);
}