using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float lifeTimer;

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
        yield return new WaitForSeconds(lifeTimer);

        Destroy(gameObject);
    }

    #region Движение

    private void Moving()
    {
        rb.velocity = transform.up * movementSpeed;
    }

    #endregion

}
