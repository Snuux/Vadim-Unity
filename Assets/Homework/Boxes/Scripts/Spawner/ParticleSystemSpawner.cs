using Boxes;
using UnityEngine;

namespace Boxes
{
    public class ParticleSystemSpawner : MonoBehaviour, ISpawner
    {
        [SerializeField] ParticleSystem _particleSystemPrefab;

        public void Initialize(ParticleSystem particleSystemPrefab)
        {
            _particleSystemPrefab = particleSystemPrefab;
        }

        public void Spawn(Vector3 position)
        {
            Instantiate(_particleSystemPrefab, position, Quaternion.identity);
        }
    }
}