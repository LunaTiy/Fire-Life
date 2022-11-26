using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Firewood : MonoBehaviour, IBurnable
{
    [SerializeField] private float _health = 1f;

    public float Health => _health;
}