using System;
using UnityEngine;

public class KinematicCharacter : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravityForce;

    [SerializeField] private ObstacleChecker _groundChecker;

    private Vector2 _movementInput;
    private bool _isJumpPressed;

    private float _yDirection;

    private void Update()
    {
        ReadInput();
    }

    private void FixedUpdate()
    {
        Vector2 xzDirection = _movementInput * _moveSpeed;

        ProcessGravity();
        ProcessJump();
        ProcessMove(new Vector3(xzDirection.x, _yDirection, xzDirection.y));
    }

    private void ProcessGravity()
    {
        if (_groundChecker.IsTouches())
            _yDirection = 0;
        else
            _yDirection -= _gravityForce * Time.fixedDeltaTime;
    }

    private void ProcessJump()
    {
        if (_isJumpPressed && _groundChecker.IsTouches())
        {
            _yDirection = _jumpForce;
            _isJumpPressed = false;
        }
    }

    private void ProcessMove(Vector3 direction)
    {
        _rigidbody.MovePosition(_rigidbody.position + direction * Time.fixedDeltaTime);
    }

    private void ReadInput()
    {
        _movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (_isJumpPressed == false && _groundChecker.IsTouches())
            _isJumpPressed = Input.GetKeyDown(KeyCode.Space);
    }
}
