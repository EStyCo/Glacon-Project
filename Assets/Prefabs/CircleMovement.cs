using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    public Transform center; // ������ ������ �������� ����� ������
    public float radius = 2.0f; // ������ ������
    public float speed = 0.5f; // �������� ��������

    private float angle = 0; // ������� ����

    void Update()
    {
        angle -= speed * Time.deltaTime;

        // ��������� ����� �������
        float x = center.position.x + radius * Mathf.Cos(angle);
        float y = center.position.y + radius * Mathf.Sin(angle);

        // ������������� ����� �������
        transform.position = new Vector3(x, y, 0);

        transform.right = -(transform.position - center.position).normalized;


    }
}