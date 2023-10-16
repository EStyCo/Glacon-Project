using UnityEngine;

public class ScaleOnMouseWheel : MonoBehaviour
{
    public float scaleSpeed = 0.1f; // Скорость изменения масштаба
    public float minScale = 0.1f; // Минимальный масштаб
    public float maxScale = 3.0f; // Максимальный масштаб

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            // Изменяем масштаб объекта
            Vector3 newScale = transform.localScale + Vector3.one * scroll * scaleSpeed;
            newScale.x = Mathf.Clamp(newScale.x, minScale, maxScale);
            newScale.y = Mathf.Clamp(newScale.y, minScale, maxScale);
            newScale.z = Mathf.Clamp(newScale.z, minScale, maxScale);
            transform.localScale = newScale;
        }
    }
}
