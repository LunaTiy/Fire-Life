using System;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiHandController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Sprite _emptyHand;
    [SerializeField] private Sprite _fullHand;
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _image;
    
    [SerializeField] private GrabChecker _grabChecker;
    [SerializeField] private GrabController _grabController;
    
    private static readonly int Notify = Animator.StringToHash("Notify");
    private bool _isNearItems;

    private void Start()
    {
        _image.sprite = _emptyHand;
    }

    private void OnEnable()
    {
        _grabChecker.OnGrabItemChanged += GrabItemChangeHandler;
        _grabController.OnHandStateSwitched += HandStateSwitchedHandler;
    }

    private void OnDisable()
    {
        _grabChecker.OnGrabItemChanged -= GrabItemChangeHandler;
        _grabController.OnHandStateSwitched -= HandStateSwitchedHandler;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_grabController.IsHold)
        {
            _grabController.Throw();
            return;
        }

        if (!_isNearItems) return;
        
        _grabController.StartAnimationGrab();
        _animator.SetBool(Notify, false);
    }

    private void GrabItemChangeHandler(bool isNear)
    {
        _isNearItems = isNear;
        _animator.SetBool(Notify, _isNearItems && !_grabController.IsHold);
    }

    private void HandStateSwitchedHandler(bool state)
    {
        _image.sprite = state ? _fullHand : _emptyHand;
    }
}