using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private GameObject _character;
    [SerializeField] private SpawnRoot _spawnRoot;
    [SerializeField] private Firecamp _firecamp;

    private Transform _baseCharacterTransform;
    
    private void Start()
    {
        _baseCharacterTransform = _character.transform;
    }

    public void StartNewGame()
    {
        SetTransform(_character.transform, _baseCharacterTransform);
        _spawnRoot.SpawnAll();
        _firecamp.ResetCamp();
    }

    private static void SetTransform(Transform currentTransform, Transform newTransform)
    {
        currentTransform.position = newTransform.position;
        currentTransform.rotation = newTransform.rotation;
        currentTransform.localScale = newTransform.localScale;
    }
}