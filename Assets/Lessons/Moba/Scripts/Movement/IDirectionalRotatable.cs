using UnityEngine;

namespace MobaLesson
{
    public interface IDirectionalRotatable : ITransformPosition
    {
        Quaternion CurrentRotation { get; }

        void SetRotationDirection(Vector3 direction);
    }
}