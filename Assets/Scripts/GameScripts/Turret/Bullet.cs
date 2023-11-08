using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private float movementSpeed = 1.8f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(TimerLife());
    }

    private void Update()
    {
        Moving();
    }

    private IEnumerator TimerLife()
    { 
        yield return new WaitForSeconds(5f);

        Destroy(gameObject);
    }

    #region Движение

    private void Moving()
    {
        rb.velocity = transform.up * movementSpeed;
    }

    #endregion

}
