using UnityEngine;

/// <summary>
/// Класс реализовывающий создание трассы
/// </summary>
public class TerrainCreator : MonoBehaviour
{
    //Родительский контейнер и ссылка на стену ограничивающую задний ход
    [SerializeField] private Transform sectionParent, backWall;

    /// <summary>
    /// Префаб одной секции трассы
    /// </summary>
    [SerializeField] private GameObject terrainSectionPrefab;
    
    /// <summary>
    /// Текущая позиция машины
    /// </summary>
    [SerializeField] private Transform carPosition;

    /// <summary>
    /// Позиция крайней секции
    /// </summary>
    private Vector3 lastCreateSectionPosition;

    /// <summary>
    /// Шаг координат секций
    /// </summary>
    private float step;

    /// <summary>
    /// Граница создания трассы
    /// </summary>
    private readonly float topBound = -3f, botBound = -9f;

    /// <summary>
    /// Количество создаваемых секций за одну итерацию
    /// </summary>
    private readonly int countOfCreate = 100;

    /// <summary>
    /// Ширина камеры
    /// </summary>
    private float cameraWidth;

    private void Start()
    {
        step = terrainSectionPrefab.transform.localScale.x;
        lastCreateSectionPosition = terrainSectionPrefab.transform.position;
        cameraWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
    }

    private void Update()
    {
        CreateSection();
        DeleteSection();
    }

    /// <summary>
    /// Создание новых секций трассы при приближении к краю
    /// </summary>
    private void CreateSection()
    {
        float distance = lastCreateSectionPosition.x - carPosition.position.x;

        if (distance <= cameraWidth)
            for (int i = 0; i < countOfCreate; i++)
            {
                float xPos = lastCreateSectionPosition.x + step;
                float yPos = GetYPosition(xPos, lastCreateSectionPosition.y);
                lastCreateSectionPosition = new Vector3(xPos, yPos, 0);
                Instantiate(terrainSectionPrefab, lastCreateSectionPosition, Quaternion.identity, sectionParent);
            }
    }

    /// <summary>
    /// Удаление секций, которые остались позади
    /// </summary>
    private void DeleteSection()
    {
        foreach (Transform child in sectionParent)
        {
            float distance = carPosition.position.x - child.position.x;

            if (distance > cameraWidth)
            {                
                backWall.position = new Vector3(child.position.x - backWall.localScale.x, backWall.position.y, backWall.position.z);
                Destroy(child.gameObject);
            }
            else
                break;
        }
    }

    /// <summary>
    /// Получение Y координаты для секции с помошью шума Перлина
    /// </summary>
    /// <param name="xPos">Текущее значение по X</param>
    /// <param name="yPos">Предыдущее значение по Y</param>
    /// <returns>Точка Y координаты</returns>
    private float GetYPosition(float xPos, float yPos)
    {
        // Вычисляем значение шума Перлина для текущей позиции
        float sample = Mathf.PerlinNoise(xPos / 6f, yPos/50f);
        // Масштабируем значение шума в диапазон от botBound до topBound
        float scaledSample = Mathf.Lerp(botBound, topBound, sample) + botBound/2;
        return scaledSample;
    }
}