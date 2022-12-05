using UnityEngine;

public class SpawnRoot : MonoBehaviour
{
    [ContextMenu("Spawn all objects")]
    private void SpawnAll()
    {
        var spawners = GetComponentsInChildren<Spawner>();

        foreach (Spawner spawner in spawners)
            spawner.Spawn();
    }
}