using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private GameObject _target;
    [Space]
    [SerializeField] private Image _joystick;
    [SerializeField] private Image _stick;

    private IControllable _controllableTarget;
    
    private Vector2 _inputVector;
    private Vector2 _basePosition;

    private void Start()
    {
        if (!_target.TryGetComponent(out _controllableTarget))
            throw new Exception("Target is not controllable");
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        _inputVector = Vector2.zero;
        _stick.rectTransform.anchoredPosition = Vector2.zero;
        
        _controllableTarget.SetDirection(Vector3.zero);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystick.rectTransform, eventData.position,
                eventData.pressEventCamera, out Vector2 position)) return;

        Vector2 joystickSizeDelta = _joystick.rectTransform.sizeDelta;
        
        position.x /= joystickSizeDelta.x;
        position.y /= joystickSizeDelta.y;

        _inputVector = new Vector2(position.x * 2 - 0.1f, position.y * 2 - 0.1f);
        _inputVector = _inputVector.magnitude > 1f ? _inputVector.normalized : _inputVector;
        
        _stick.rectTransform.anchoredPosition = new Vector2(_inputVector.x * (joystickSizeDelta.x / 2),
            _inputVector.y * (joystickSizeDelta.y / 2));
        
        _controllableTarget.SetDirection(new Vector3(_inputVector.x, 0, _inputVector.y));
    }
}