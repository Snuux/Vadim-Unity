using UnityEngine;

public interface IAgentRotatable : ITransformPosition
{
    void SetRotationDirection(Vector3 direction);
}