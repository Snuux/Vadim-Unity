using System;
using UnityEngine;

public class MouseDragger : IDragger
{
    private const float DefaultRayDistance = 100f;
    private const float Treshhold = 0.1f;

    private IDraggable _draggable;
    private float _yDragPosition;
    private bool _isDragging;

    public MouseDragger(float yDragPosition)
    {
        _yDragPosition = yDragPosition;
    }

    public void Start()
    {
        _draggable = GetDraggableUnderMousePosition();

        if (_draggable != null)
        {
            _draggable.OnGrab();
            _isDragging = true;
        }
    }

    public void FixedUpdate()
    {
        if (_isDragging == false)
            return;

        Ray ray = ScreenPointToRay();
        Vector3 mouseWorldPos = MouseWorldPosition();
        Vector3 hitPoint = GetHitPoint(ray);

        Vector3 direction = (mouseWorldPos - hitPoint).normalized;

        if (direction.magnitude > Treshhold)
        {
            //t - это точка на плоскости на высоте _yDragPosition с которой сталкивается луч
            //вычисляется из уравнения прямой P(t) = origin + direction * t
            //и отсюда t = (yPos - origin.y)/direction.y

            float t = (_yDragPosition - ray.origin.y) / ray.direction.y;
            _draggable.OnDrag(ray.origin + ray.direction * t);
        }
    }

    private static Vector3 GetHitPoint(Ray ray)
    {
        Vector3 hitPoint = Vector3.zero;

        if (Physics.Raycast(ray, out RaycastHit hit, DefaultRayDistance))
            hitPoint = hit.point;
        return hitPoint;
    }

    public void Stop()
    {
        _isDragging = false;

        if (_draggable != null)
        {
            _draggable.OnRelease();
            _draggable = null;
        }
    }

    private IDraggable GetDraggableUnderMousePosition()
    {
        if (!Physics.Raycast(ScreenPointToRay(), out RaycastHit hit, DefaultRayDistance))
            return null;

        if (!hit.collider.TryGetComponent<IDraggable>(out IDraggable draggable))
            return null;

        return draggable;
    }

    private Ray ScreenPointToRay() => Camera.main.ScreenPointToRay(Input.mousePosition);

    private Vector3 MouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(
                    new Vector3(Input.mousePosition.x, Input.mousePosition.y, DefaultRayDistance)
                );
    }
}