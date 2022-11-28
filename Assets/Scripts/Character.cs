using System;
using UnityEngine;

[RequireComponent(typeof(Character), typeof(Animator))]
public class Character : MonoBehaviour, IControllable, IViewable
{
    public event Action OnChangedPosition;

    [Header("References")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    
    [Space] [Header("Character characteristics")]
    [SerializeField] private float _speed;
    [SerializeField] private float _angularSpeed;
    [SerializeField] private float _gravityForce;
    [SerializeField] private float _dashSpeed = 70f;
    [SerializeField] private float _dashDuration = 2f;
    [SerializeField] private float _dashReload = 5f;

    private static readonly int Walk = Animator.StringToHash("Walk");

    private float _gravityAcceleration;
    private Vector3 _moveVector;
    private bool _isReloadingDash;
    private float _savedSpeedAfterDash;

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

    public void Dash()
    {
        if (_isReloadingDash)
            return;

        _savedSpeedAfterDash = _speed;
        _speed = _dashSpeed;
        
        Move();
        
        _isReloadingDash = true;
        Invoke(nameof(StopDash), _dashDuration);
    }

    private void StopDash()
    {
        CancelInvoke(nameof(StopDash));

        _speed = _savedSpeedAfterDash;
        Invoke(nameof(ResetDash), _dashReload);
    }

    private void ResetDash()
    {
        CancelInvoke(nameof(ResetDash));
        _isReloadingDash = false;
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