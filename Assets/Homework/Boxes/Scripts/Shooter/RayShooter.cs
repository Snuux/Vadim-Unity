using UnityEngine;

public class RayShooter : IShooter
{
    private const float MaxRayDistance = 100f;

    private IShootEffect _shooterEffect;

    public RayShooter(IShootEffect shootEffect)
    {
        _shooterEffect = shootEffect;
    }

    public void Shoot(Vector3 origin, Vector3 direction)
    {
        Ray ray = new Ray(origin, direction);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, MaxRayDistance))
        {
            _shooterEffect.Execute(hitInfo.point);
        }
    }
}