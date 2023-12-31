using System.Collections;
using UnityEngine;
using Zenject;

public class Bullet : MonoBehaviour
{
    [Inject] ShipConstructor shipConstructor;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(TimerLife());
    }

    private void Update()
    {
        Moving();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bullet bullet))
        { 
            Destroy(gameObject);
            Destroy(bullet.gameObject);
        }
    }

    private IEnumerator TimerLife()
    {
        yield return new WaitForSeconds(shipConstructor.rangeFly);

        Destroy(gameObject);
    }

    #region Moving

    private void Moving()
    {
        rb.velocity = transform.up * shipConstructor.bulletSpeed;
    }

    #endregion

}
