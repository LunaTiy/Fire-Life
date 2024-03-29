﻿using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Firewood : MonoBehaviour, IBurnable, IGrabAvailable
{
    public event Action OnBurned;

    [SerializeField] private float _health = 1f;

    public float Health => _health;

    private void OnDestroy()
    {
        OnBurned?.Invoke();
    }

    public GameObject GetItem() => gameObject;
}