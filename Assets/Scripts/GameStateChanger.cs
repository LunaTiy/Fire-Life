using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameStateChanger : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Image _gameStateChanger;
    [SerializeField] private Sprite _activeState;
    [SerializeField] private Sprite _pauseState;

    private void OnEnable()
    {
        _gameManager.OnChangedGameState += ChangeGameStateHandler;
    }

    private void OnDisable()
    {
        _gameManager.OnChangedGameState -= ChangeGameStateHandler;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _gameManager.ChangeGameState();
    }

    private void ChangeGameStateHandler(bool state)
    {
        _gameStateChanger.sprite = state ? _pauseState : _activeState;
    }
}