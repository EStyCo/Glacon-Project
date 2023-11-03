using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private float movementSpeed = 3.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Moving();
    }

    #region Движение

    private void Moving()
    {
        rb.velocity = transform.up * movementSpeed;
    }

    #endregion

}
