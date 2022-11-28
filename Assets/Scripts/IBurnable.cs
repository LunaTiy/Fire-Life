using System;

public interface IBurnable
{
    public event Action OnBurned;
    float Health { get; }
}