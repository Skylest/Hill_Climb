using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// Класс реализовывающий "зажимание" кнопки
/// </summary>
public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    /// <summary>
    /// Ивент при зажатии кнопки
    /// </summary>
    [SerializeField] public UnityEvent onHoldEvent;
    
    /// <summary>
    /// Ивент при отпускании кнопки
    /// </summary>
    [SerializeField] public UnityEvent onUpEvent;

    //Обработчик нажатия на кнопку 
    public void OnPointerDown(PointerEventData eventData)
    {
        InvokeRepeating(nameof(HoldAction), 0f, 0.1f);
    }

    //Обработчик отпускания кнопки
    public void OnPointerUp(PointerEventData eventData)
    {
        CancelInvoke(nameof(HoldAction));
        onUpEvent?.Invoke();
    }

    /// <summary>
    /// Метод выполняющийся при зажатии кнопки
    /// </summary>
    private void HoldAction()
    {
        onHoldEvent?.Invoke();
    }
}
