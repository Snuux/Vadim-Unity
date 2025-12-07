using UnityEngine;

public interface IExplosionSource : ITransformPosition
{
    float Radius { get; }
    bool IsExploded { get; }

    void Explode(Vector3 point);
}
