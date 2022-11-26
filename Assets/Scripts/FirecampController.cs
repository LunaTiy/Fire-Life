using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class FirecampController : MonoBehaviour
{
    public event Action OnHealthChanged;
    public event Action OnCampDie;
    
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _lossHealthPerSecond = 2f;
    [Space]
    [SerializeField] private Transform _logsParent;

    private Transform[] _logs;
    private int _countLogs;
    private int _countActiveLogs;
    private float _healthOneLog;
    private bool _isBurn = true;

    public float Health
    {
        get => _health;
        private set
        {
            if (value <= 0)
            {
                _health = 0f;
                _isBurn = false;
                
                OnCampDie?.Invoke();
                StopCoroutine(LoseHealthRoutine());
            }
            else if (value > _maxHealth)
            {
                _health = _maxHealth;
            }
            else
            {
                _health = value;
            }

            ChangeLogsCount();
            OnHealthChanged?.Invoke();
        }
    }

    private void Start()
    {
        GetLogs();
        StartCoroutine(LoseHealthRoutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent(out IBurnable burnable))
            return;
    
        Health += burnable.Health;
        Destroy(other.gameObject);
    }

    private void GetLogs()
    {
        if (_logsParent == null)
            throw new NullReferenceException("Logs parent is null reference");

        _countLogs = _logsParent.childCount;
        _countActiveLogs = _countLogs;
        _logs = new Transform[_countLogs];

        for (var i = 0; i < _countLogs; i++)
            _logs[i] = _logsParent.GetChild(i);
    }

    private void ChangeLogsCount()
    {
        int targetCountLogs = Mathf.RoundToInt(Health / _maxHealth * _countLogs);

        if (targetCountLogs == _countActiveLogs)
            return;
        
        if (targetCountLogs < _countActiveLogs)
        {
            for(int i = _countActiveLogs - 1; i >= targetCountLogs; i--)
                _logs[i].gameObject.SetActive(false);
        }
        else
        {
            for(int i = targetCountLogs - 1; i >= _countActiveLogs; i--)
                _logs[i].gameObject.SetActive(true);
        }

        _countActiveLogs = targetCountLogs;
    }

    private IEnumerator LoseHealthRoutine()
    {
        WaitForSeconds oneSecond = new WaitForSeconds(1f);

        while (_isBurn)
        {
            Health -= _lossHealthPerSecond;

            yield return oneSecond;
        }

        yield return null;
    }
}