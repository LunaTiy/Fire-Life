using System;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    public event Action<bool> OnHandStateSwitched;
    
    private static readonly int GrabParameter = Animator.StringToHash("Grab");

    [SerializeField] private Transform _hand;
    [SerializeField] private GrabChecker _grabChecker;
    [SerializeField] private Animator _animator;

    private GameObject _holdenItem;

    public bool IsHold { get; private set; }

    public void StartAnimationGrab()
    {
        _animator.SetTrigger(GrabParameter);
    }

    public void Grab()
    {
        IGrabAvailable grabItem = _grabChecker.GetFirstGrabAvailableItem();

        if (grabItem == null)
            return;

        GameObject item = grabItem.GetItem();
        
        item.transform.SetParent(_hand, false);
        ResetLocalTransform(item.transform);

        _holdenItem = item;
        IsHold = true;

        _holdenItem.GetComponent<IBurnable>().OnBurned += OnBurnHandler;
        
        OnHandStateSwitched?.Invoke(IsHold);
    }

    public void Throw()
    {
        _holdenItem.GetComponent<IBurnable>().OnBurned -= OnBurnHandler;

        Vector3 scale = _holdenItem.transform.localScale;
        _holdenItem.transform.parent = null;
        _holdenItem.transform.localScale = scale;

        _holdenItem = null;
        IsHold = false;
        
        OnHandStateSwitched?.Invoke(IsHold);
    }

    private static void ResetLocalTransform(Transform objTransform)
    {
        objTransform.localPosition = Vector3.zero;
        objTransform.localRotation = Quaternion.identity;
    }

    private void OnBurnHandler()
    {
        _holdenItem = null;
        IsHold = false;
        
        OnHandStateSwitched?.Invoke(IsHold);
    }
}