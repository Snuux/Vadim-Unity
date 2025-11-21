using System;
using UnityEngine;

public class NonKinematicCharacter : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    [SerializeField] private ObstacleChecker _groundChecker;

    private Vector2 _movementInput;
    private bool _isJumpPressed;

    private void Update()
    {
        ReadInput();
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(new Vector3(_movementInput.x, 0, _movementInput.y) * _moveSpeed);
    
        if (_isJumpPressed && _groundChecker.IsTouches())
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isJumpPressed = false;
        }
    }

    private void ReadInput()
    {
        _movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (_isJumpPressed == false)
            _isJumpPressed = Input.GetKeyDown(KeyCode.Space);
    }
}
