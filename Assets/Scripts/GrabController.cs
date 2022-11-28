using System;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    public event Action OnReleaseHand;
    [SerializeField] private Transform _hand;
    [SerializeField] private GrabChecker _grabChecker;

    private GameObject _holdenItem;
    
    public bool IsHold { get; private set; }

    public void Grab()
    {
        GameObject item = _grabChecker.GetFirstItem();

        if (item == null)
            return;
        
        item.transform.SetParent(_hand, false);
        ResetLocalTransform(item.transform);

        _holdenItem = item;
        IsHold = true;

        _holdenItem.GetComponent<IBurnable>().OnBurned += OnBurnHandler;
    }

    public void Throw()
    {
        _holdenItem.GetComponent<IBurnable>().OnBurned -= OnBurnHandler;

        Vector3 scale = _holdenItem.transform.localScale;
        _holdenItem.transform.parent = null;
        _holdenItem.transform.localScale = scale;

        _holdenItem = null;
        IsHold = false;
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
        
        OnReleaseHand?.Invoke();
    }
}