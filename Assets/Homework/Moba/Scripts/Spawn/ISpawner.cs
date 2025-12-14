using UnityEngine;
using UnityEngine.UIElements;

public abstract class Spawner : MonoBehaviour 
{
    public abstract void Spawn(Vector3 position);
}
