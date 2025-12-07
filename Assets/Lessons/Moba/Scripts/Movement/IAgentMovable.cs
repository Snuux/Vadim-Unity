using UnityEngine;

namespace MobaLesson
{
    public interface IAgentMovable : ITransformPosition
    {
        Vector3 CurrentVelocity { get; }

        void SetDestiantion(Vector3 direction);
    }
}