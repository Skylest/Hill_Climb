using UnityEngine;

/// <summary>
/// Класс реализовывающий следование камеры за объектом
/// </summary>
public class CameraFollowing : MonoBehaviour
{
    /// <summary>
    /// Объект за которым следуем
    /// </summary>
    [SerializeField] private GameObject folowingObject;

    /// <summary>
    /// Переменная максимальной позиции камеры по X
    /// </summary>
    private float maxX = 0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 vector = new Vector3(folowingObject.transform.position.x, folowingObject.transform.position.y, transform.position.z);
        
        //Ограничиваем обратный ход камеры
        if (folowingObject.transform.position.x <= maxX)
            vector.x = maxX;
        else
            maxX = vector.x;

        transform.position = vector;
    }
}
