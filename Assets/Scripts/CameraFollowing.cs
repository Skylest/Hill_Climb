using UnityEngine;

/// <summary>
/// ����� ��������������� ���������� ������ �� ��������
/// </summary>
public class CameraFollowing : MonoBehaviour
{
    /// <summary>
    /// ������ �� ������� �������
    /// </summary>
    [SerializeField] private GameObject folowingObject;

    /// <summary>
    /// ���������� ������������ ������� ������ �� X
    /// </summary>
    private float maxX = 0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 vector = new Vector3(folowingObject.transform.position.x, folowingObject.transform.position.y, transform.position.z);
        
        //������������ �������� ��� ������
        if (folowingObject.transform.position.x <= maxX)
            vector.x = maxX;
        else
            maxX = vector.x;

        transform.position = vector;
    }
}
