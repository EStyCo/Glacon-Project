using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private float movementSpeed = 5f;

    private void Start()
    {
        StartCoroutine(CorrectAngleTracking());
    }

    private void Update()
    {
        Moving();
    }

    #region Движение

    private void Moving()
    {
        if (target != null)
        {
            Vector3 newPosition = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
            transform.position = newPosition;
        }
        else Destroy(gameObject);
    }

    protected IEnumerator CorrectAngleTracking()
    {
        while (true)
        {
            if (target != null)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
                transform.rotation = targetRotation;
            }

            yield return new WaitForSeconds(0.025f);
        }
    }

    #endregion

    public void SetTarget(Transform targetShip)
    {
        target = targetShip;
    }
}
