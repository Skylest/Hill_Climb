using UnityEngine;

/// <summary>
/// ����� ��������������� ������ ������
/// </summary>
public class WheelController : MonoBehaviour
{
    private WheelJoint2D wheelJoint;
    
    /// <summary>
    /// ��������� ������ ��� ������
    /// </summary>
    private JointMotor2D motorForward, motorBackward;

    /// <summary>
    /// ���������
    /// </summary>
    private readonly float accelerationSpeed = 1000f;

    /// <summary>
    /// �������� ����������/������� ����
    /// </summary>
    private readonly float brakeSpeed = 400f;

    /// <summary>
    /// ���� ������� �����
    /// </summary>
    public bool IsOnGround { get; private set; }

    private void Start()
    {
        wheelJoint = GetComponent<WheelJoint2D>();

        motorForward = new JointMotor2D
        {
            motorSpeed = accelerationSpeed,
            maxMotorTorque = 10000
        };

        motorBackward = new JointMotor2D
        {
            motorSpeed = -brakeSpeed,
            maxMotorTorque = 10000
        };
    }

    /// <summary>
    /// ���������
    /// </summary>
    public void Accelerate()
    {
        wheelJoint.motor = motorForward;
        wheelJoint.useMotor = true;
    }

    /// <summary>
    /// ����������/������ ���
    /// </summary>
    public void Brake()
    {
        wheelJoint.motor = motorBackward;
        wheelJoint.useMotor = true;
    }

    /// <summary>
    /// �������
    /// </summary>
    public void Stay()
    {
        wheelJoint.useMotor = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = false;
        }
    }
}
