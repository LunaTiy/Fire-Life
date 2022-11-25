using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private Vector3 _offset;

    private IViewable _viewableTarget;

    private void Awake()
    {
        if (!_target.TryGetComponent(out _viewableTarget))
            throw new Exception("Target is not viewable");
    }

    private void OnEnable()
    {
        _viewableTarget.OnChangedPosition += ChangedPositionHandler;
    }

    private void OnDisable()
    {
        _viewableTarget.OnChangedPosition -= ChangedPositionHandler;
    }

    private void ChangedPositionHandler()
    {
        transform.position = _viewableTarget.Position + _offset;
    }
}