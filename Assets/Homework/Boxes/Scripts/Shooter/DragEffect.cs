using UnityEngine;

public class DragEffect : IShootEffect
{
    private const float SmallRadius = 0.01f;

    private SimpleDraggable _draggable;
    private Spawner _simpleDraggableAppender;
    private LayerMask _layerMask;

    public DragEffect(Spawner simpleDraggable, LayerMask layerMask)
    {
        _simpleDraggableAppender = simpleDraggable;
        _layerMask = layerMask;
    }

    public void Execute(Vector3 point)
    {
        Collider[] targets = Physics.OverlapSphere(point, SmallRadius, _layerMask);

        if (targets.Length == 0)
            return;

        if (targets[0].TryGetComponent(out Rigidbody rigidbody) == false)
        {
            Debug.Log("No rigidbody on: " + targets[0].name);
            return;
        }

        _draggable = _simpleDraggableAppender.Spawn(point).GetComponent<SimpleDraggable>();
        _draggable.Initialize(targets[0].GetComponent<Rigidbody>());
        _draggable.BeginDrag();
    }

    public void StopExecute()
    {
        if (_draggable != null)
            _draggable.EndDrag();
    }
}