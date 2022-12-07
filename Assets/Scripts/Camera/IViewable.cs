using System;
using UnityEngine;

public interface IViewable
{
    event Action OnChangedPosition;
    Vector3  Position { get; }
}