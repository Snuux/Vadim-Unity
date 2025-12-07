using UnityEngine;

namespace Boxes
{
    public interface IExplodable
    {
        void Explode(Vector3 point, Vector3 force);
    }
}