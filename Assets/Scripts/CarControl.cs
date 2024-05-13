using UnityEngine;

/// <summary>
/// Класс реализовывающий управление машиной
/// </summary>
public class CarControl : MonoBehaviour
{
    /// <summary>
    /// Контроллер колеса
    /// </summary>
    [SerializeField] private WheelController wheelRear, wheelFront;
    
    /// <summary>
    /// Флаг полноприводная машина или заднеприводная
    /// </summary>
    [SerializeField] private bool IsAwdCar = false;

    /// <summary>
    /// Скорость поворота в воздухе
    /// </summary>
    private readonly float rotationSpeed = 2f;

    /// <summary>
    /// Rigidbody объекта
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
    /// Ускорение
    /// </summary>
    public void Accelerate()
    {
        isAccelerate = true;        
    }

    /// <summary>
    /// Торможение/задний ход
    /// </summary>
    public void Brake()
    {
        isBrake = true;
    }

    /// <summary>
    /// Ускорение
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
    /// Торможение/задний ход
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
    /// Простой
    /// </summary>
    public void Stay()
    {
        isAccelerate = false;
        isBrake = false;
        wheelRear.Stay();
        wheelFront.Stay();
    }

    /// <summary>
    /// Поднять машину в случаи застревания
    /// </summary>
    public void ResetPosition()
    {
        transform.SetPositionAndRotation(new Vector3(transform.position.x, 3.9f, transform.position.z), Quaternion.identity);
    }

    /// <summary>
    /// Вращение машины в воздухе
    /// </summary>
    /// <param name="direction">Направление вращения (-1 или 1)</param>
    private void Rotate(int direction)
    {
        car.AddTorque(direction * rotationSpeed);
    }
}
