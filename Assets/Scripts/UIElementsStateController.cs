using UnityEngine;
using UnityEngine.UI;

public class UIElementsStateController : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _controlElements;

    private void OnEnable()
    {
        _gameManager.OnChangedGameState += ChangeGameStateHandler;
    }

    private void OnDisable()
    {
        _gameManager.OnChangedGameState -= ChangeGameStateHandler;
    }

    private void ChangeGameStateHandler(bool state)
    {
        _menuPanel.SetActive(!state);
        _controlElements.SetActive(state);
    }
}
