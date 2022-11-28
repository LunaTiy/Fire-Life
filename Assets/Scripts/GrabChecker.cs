using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GrabChecker : MonoBehaviour
{
    public event Action<bool> OnGrabItemChanged;
    
    private readonly List<GameObject> _nearItems = new List<GameObject>();
    public bool IsNearItem => _nearItems.Count > 0;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent(out IBurnable _))
            return;

        if (_nearItems.Contains(other.gameObject))
            return;
        
        _nearItems.Add(other.gameObject);
        OnGrabItemChanged?.Invoke(IsNearItem);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!_nearItems.Contains(other.gameObject))
            return;

        _nearItems.Remove(other.gameObject);
        OnGrabItemChanged?.Invoke(IsNearItem);
    }

    public GameObject GetFirstItem()
    {
        if (!IsNearItem)
            return null;
        
        GameObject item = _nearItems[0];
        _nearItems.RemoveAt(0);
        
        return item;
    }
}