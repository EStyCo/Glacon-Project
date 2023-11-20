using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    public Transform center; // объект вокруг которого летит объект
    public float radius = 2.0f; // радиус орбиты
    public float speed = 0.5f; // скорость вращения

    private float angle = 0; // текущий угол

    void Update()
    {
        angle -= speed * Time.deltaTime;

        // вычисляем новую позицию
        float x = center.position.x + radius * Mathf.Cos(angle);
        float y = center.position.y + radius * Mathf.Sin(angle);

        // устанавливаем новую позицию
        transform.position = new Vector3(x, y, 0);

        transform.right = -(transform.position - center.position).normalized;


    }
}