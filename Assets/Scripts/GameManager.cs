using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public event Action<bool> OnChangedGameState;

    [SerializeField] private Firecamp _firecamp;

    private bool _canPlay;
    private bool _isPlaying;

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
        _canPlay = true;
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

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        Time.timeScale = 1f;
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