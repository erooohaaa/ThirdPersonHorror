using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIVirtualButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    [Header("Output")]
    public BoolEvent buttonStateOutputEvent;
    public UnityEvent buttonClickOutputEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        OutputButtonStateValue(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OutputButtonStateValue(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OutputButtonClickEvent();
    }

    private void OutputButtonStateValue(bool buttonState)
    {
        buttonStateOutputEvent.Invoke(buttonState);
    }

    private void OutputButtonClickEvent()
    {
        buttonClickOutputEvent.Invoke();
    }
}