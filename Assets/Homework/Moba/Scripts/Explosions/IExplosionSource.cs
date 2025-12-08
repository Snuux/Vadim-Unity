public interface IExplosionSource : ITransformPosition
{
    float Radius { get; }

    void Explode();
}