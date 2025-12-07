using UnityEngine;

namespace MobaLesson
{
    public interface IDirectionalMovable : ITransformPosition
    {
        Vector3 CurrentVelocity { get; }

        void SetMoveDirection(Vector3 direction);
    }
}