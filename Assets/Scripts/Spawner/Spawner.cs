using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using Random = UnityEngine.Random;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "VirtualMemberNeverOverridden.Global")]
public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected int minTimeReload;
    [SerializeField] protected int maxTimeReload;
    [SerializeField] protected int maxCountObjects;

    [SerializeField] protected GameObject[] _prefabs;

    protected virtual int CountObjects => transform.childCount;
    
    protected bool canSpawn = true;

    protected void Start()
    {
        if (minTimeReload > maxTimeReload)
            throw new ArgumentException($"Min time: {minTimeReload} is greater max time: {maxTimeReload}");

        StartCoroutine(SpawnRoutine());
    }

    protected virtual IEnumerator SpawnRoutine()
    {
        while (canSpawn)
        {
            Spawn();
            yield return new WaitForSeconds(Random.Range(minTimeReload, maxTimeReload));
        }

        yield return null;
    }

    [ContextMenu("Spawn Object")]
    public virtual void Spawn()
    {
        if (CountObjects >= maxCountObjects) return;
        
        if (_prefabs == null || _prefabs.Length == 0)
            throw new NullReferenceException("Prefab null reference exception");
                
        GameObject prefabForInstantiate = _prefabs[Random.Range(0, _prefabs.Length)];

        GameObject obj = Instantiate(prefabForInstantiate, Vector3.zero, Quaternion.identity, transform);
        ResetLocalTransform(obj.transform);
    }
    
    protected virtual void ResetLocalTransform(Transform transformObj)
    {
        transformObj.localPosition = Vector3.zero;
        transformObj.localRotation = new Quaternion(0, Random.Range(0, 360), 0, 0);
        transformObj.localScale = Vector3.one;
    }
}