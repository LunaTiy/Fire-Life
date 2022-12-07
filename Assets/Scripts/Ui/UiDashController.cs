using UnityEngine;
using UnityEngine.EventSystems;

public class UiDashController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Character _character;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _character.Dash();
    }
}
