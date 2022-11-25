using UnityEngine;

public interface IControllable
{
    float Speed { get; }
    void SetDirection(Vector3 direction);
}