using System;
using UnityEngine;

[RequireComponent(typeof(Character), typeof(Animator))]
public class Character : MonoBehaviour, IControllable, IViewable
{
    public event Action OnChangedPosition;

    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    [Space] [SerializeField] private float _speed;
    [SerializeField] private float _angularSpeed;
    [SerializeField] private float _gravityForce;

    private float _gravityAcceleration;
    private Vector3 _moveVector;
    private static readonly int Walk = Animator.StringToHash("Walk");

    public float Speed => _speed;
    public Vector3 Position => transform.position;

    private void Update()
    {
        SetGravityAcceleration();
        Move();
        Rotate();
    }

    public void SetDirection(Vector3 direction)
    {
        _moveVector = direction;
    }

    private void Move()
    {
        Vector3 direction = _moveVector;

        if (!_characterController.isGrounded)
            direction = new Vector3(0, _gravityAcceleration * Time.deltaTime, 0);
        else if (direction != Vector3.zero)
            _animator.SetBool(Walk, true);
        else
            _animator.SetBool(Walk, false);

        _characterController.Move(direction * (_speed * Time.deltaTime));

        OnChangedPosition?.Invoke();
    }

    private void Rotate()
    {
        if (!_characterController.isGrounded)
            return;

        Vector3 direct = Vector3.RotateTowards(transform.forward, _moveVector, _angularSpeed * Time.deltaTime, 0f);
        transform.rotation = Quaternion.LookRotation(direct);
    }

    private void SetGravityAcceleration()
    {
        if (_characterController.isGrounded)
        {
            _gravityAcceleration = -1f;
            return;
        }

        _gravityAcceleration -= _gravityForce;
    }
}