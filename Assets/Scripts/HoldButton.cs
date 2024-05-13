using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// ����� ��������������� "���������" ������
/// </summary>
public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    /// <summary>
    /// ����� ��� ������� ������
    /// </summary>
    [SerializeField] public UnityEvent onHoldEvent;
    
    /// <summary>
    /// ����� ��� ���������� ������
    /// </summary>
    [SerializeField] public UnityEvent onUpEvent;

    //���������� ������� �� ������ 
    public void OnPointerDown(PointerEventData eventData)
    {
        InvokeRepeating(nameof(HoldAction), 0f, 0.1f);
    }

    //���������� ���������� ������
    public void OnPointerUp(PointerEventData eventData)
    {
        CancelInvoke(nameof(HoldAction));
        onUpEvent?.Invoke();
    }

    /// <summary>
    /// ����� ������������� ��� ������� ������
    /// </summary>
    private void HoldAction()
    {
        onHoldEvent?.Invoke();
    }
}
