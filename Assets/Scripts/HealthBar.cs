using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteAlways]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _fillArea;
    [SerializeField] private Firecamp _firecamp;

    private void OnEnable()
    {
        _firecamp.OnHealthChanged += HealthChangeHandler;
        HealthChangeHandler();
    }

    private void OnDisable()
    {
        _firecamp.OnHealthChanged -= HealthChangeHandler;
    }

    private void HealthChangeHandler()
    {
        _text.text = $"{Mathf.RoundToInt(_firecamp.Health)} / {Mathf.RoundToInt(Firecamp.MaxHealth)}";
        _fillArea.fillAmount = _firecamp.Health / Firecamp.MaxHealth;
    }
}