using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioStateChanger : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private AudioListener _audioListener;
    [SerializeField] private Image _audioStateChanger;
    [SerializeField] private Sprite _soundOnState;
    [SerializeField] private Sprite _soundOffState;

    public void OnPointerUp(PointerEventData eventData)
    {
        bool isEnable = !_audioListener.enabled;
        
        _audioListener.enabled = isEnable;
        _audioStateChanger.sprite = isEnable ? _soundOnState : _soundOffState;
    }
}