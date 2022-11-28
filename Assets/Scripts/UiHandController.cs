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
    private bool _canTake;

    private void Start()
    {
        _image.sprite = _emptyHand;
    }

    private void OnEnable()
    {
        _grabChecker.OnGrabItemChanged += GrabItemChangeHandler;
        _grabController.OnReleaseHand += ReleaseHandHandler;
    }

    private void OnDisable()
    {
        _grabChecker.OnGrabItemChanged -= GrabItemChangeHandler;
        _grabController.OnReleaseHand -= ReleaseHandHandler;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_grabController.IsHold)
        {
            _grabController.Throw();
            _image.sprite = _emptyHand;
            _animator.SetBool(Notify, _grabChecker.IsNearItem);

            return;
        }

        if (!_canTake) return;
        
        _grabController.Grab();
        _image.sprite = _fullHand;
        
        _animator.SetBool(Notify, false);
    }

    private void GrabItemChangeHandler(bool isNear)
    {
        _canTake = isNear && !_grabController.IsHold;

        _animator.SetBool(Notify, _canTake);
    }

    private void ReleaseHandHandler()
    {
        _image.sprite = _emptyHand;
        _animator.SetBool(Notify, false);
    }
}