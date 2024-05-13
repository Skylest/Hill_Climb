using UnityEngine;

/// <summary>
/// ����� ��������������� ���������� �������
/// </summary>
public class CarControl : MonoBehaviour
{
    /// <summary>
    /// ���������� ������
    /// </summary>
    [SerializeField] private WheelController wheelRear, wheelFront;
    
    /// <summary>
    /// ���� �������������� ������ ��� ��������������
    /// </summary>
    [SerializeField] private bool IsAwdCar = false;

    /// <summary>
    /// �������� �������� � �������
    /// </summary>
    private readonly float rotationSpeed = 2f;

    /// <summary>
    /// Rigidbody �������
    /// </summary>
    private Rigidbody2D car;

    private bool isAccelerate = false;
    private bool isBrake = false;

    // Start is called before the first frame update
    void Start()
    {
        car = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isAccelerate)
            _Accelerate();
       else if (isBrake)
            _Brake();
    }

    /// <summary>
    /// ���������
    /// </summary>
    public void Accelerate()
    {
        isAccelerate = true;        
    }

    /// <summary>
    /// ����������/������ ���
    /// </summary>
    public void Brake()
    {
        isBrake = true;
    }

    /// <summary>
    /// ���������
    /// </summary>
    private void _Accelerate()
    {
        if (wheelRear.IsOnGround || wheelFront.IsOnGround)
        {
            wheelRear.Accelerate();
            if (IsAwdCar)
                wheelFront.Accelerate();
        }
        else
            Rotate(-1);
    }

    /// <summary>
    /// ����������/������ ���
    /// </summary>
    private void _Brake()
    {
        if (wheelRear.IsOnGround || wheelFront.IsOnGround)
        {
            wheelRear.Brake();
            wheelFront.Brake();
        }
        else
            Rotate(1);
    }

    /// <summary>
    /// �������
    /// </summary>
    public void Stay()
    {
        isAccelerate = false;
        isBrake = false;
        wheelRear.Stay();
        wheelFront.Stay();
    }

    /// <summary>
    /// ������� ������ � ������ �����������
    /// </summary>
    public void ResetPosition()
    {
        transform.SetPositionAndRotation(new Vector3(transform.position.x, 3.9f, transform.position.z), Quaternion.identity);
    }

    /// <summary>
    /// �������� ������ � �������
    /// </summary>
    /// <param name="direction">����������� �������� (-1 ��� 1)</param>
    private void Rotate(int direction)
    {
        car.AddTorque(direction * rotationSpeed);
    }
}
