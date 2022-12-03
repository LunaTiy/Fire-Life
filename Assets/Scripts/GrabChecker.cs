using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GrabChecker : MonoBehaviour
{
    public event Action<bool> OnGrabItemChanged;
    
    private readonly List<IGrabAvailable> _nearItems = new List<IGrabAvailable>();
    private bool IsNearItem => _nearItems.Count > 0;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent(out IGrabAvailable item))
            return;

        if (_nearItems.Contains(item))
            return;
        
        _nearItems.Add(item);
        OnGrabItemChanged?.Invoke(IsNearItem);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out IGrabAvailable grabAvailableItem))
            return;
        
        if (!_nearItems.Contains(grabAvailableItem))
            return;

        _nearItems.Remove(grabAvailableItem);
        OnGrabItemChanged?.Invoke(IsNearItem);
    }

    public IGrabAvailable GetFirstGrabAvailableItem()
    {
        if (!IsNearItem)
            return null;
        
        IGrabAvailable item = _nearItems[0];
        _nearItems.RemoveAt(0);
        
        return item;
    }

    public void TryRemoveItem(IGrabAvailable item)
    {
        if (_nearItems.Contains(item))
            _nearItems.Remove(item);
    }
}