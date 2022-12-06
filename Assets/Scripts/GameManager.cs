using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public event Action<bool> OnChangedGameState;

    [SerializeField] private Firecamp _firecamp;

    private bool _canPlay;
    private bool _isPlaying;
    private float _gameTimeScale;

    public bool IsPlaying
    {
        get => _isPlaying;
        private set
        {
            _isPlaying = value;
            OnChangedGameState?.Invoke(_isPlaying);
        }
    }

    private void Start()
    {
        _gameTimeScale = Time.timeScale;
        Pause();
    }

    private void OnEnable()
    {
        _firecamp.OnCampDie += CampDieHandler;
    }

    private void OnDisable()
    {
        _firecamp.OnCampDie -= CampDieHandler;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        _canPlay = true;
        IsPlaying = true;
    }

    public void Pause()
    {
        IsPlaying = false;
        Time.timeScale = 0f;
    }

    public void Play()
    {
        if (!_canPlay)
            return;
        
        IsPlaying = true;
        Time.timeScale = _gameTimeScale;
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void CampDieHandler()
    {
        EndGame();
    }

    private void EndGame()
    {
        _canPlay = false;
        Pause();
    }
}