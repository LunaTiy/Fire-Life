using UnityEngine;

public class Tree : MonoBehaviour, IGrabAvailable
{
    [SerializeField] private GameObject _logPrefab;
    
    public GameObject GetItem()
    {
        Destroy(gameObject);

        GameObject log = Instantiate(_logPrefab, transform.position, Quaternion.identity);
        return log;
    }
}