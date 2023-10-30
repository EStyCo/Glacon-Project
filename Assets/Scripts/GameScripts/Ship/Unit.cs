using System.Collections;
using UnityEngine;

public class Unit : Ship
{
    #region Triggers and Collisions

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag != gameObject.tag)
        {
            TryAvoidCollision(1, damage);
        }
    }

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("BlackHole"))
        {
            if (!isAbsorb)
            {
                StartCoroutine((Absorb(collision)));
            }
        }

        if (targetPlanet != null &&
            collision.gameObject == targetPlanet.gameObject &&
            gameObject.tag != collision.gameObject.tag)
        {
            targetPlanet.DecreaseUnits();
            ChangeTagPlanet();

            if (Random.Range(0, 101) < damage)
            {
                targetPlanet.DecreaseUnits();
            }

            ChangeTagPlanet();
            StartCoroutine(Destruction());
        }
        else if (targetPlanet != null &&
                 collision.gameObject == targetPlanet.gameObject &&
                 gameObject.tag == collision.gameObject.tag)
        {
            targetPlanet.IncreaseUnits();

            SpriteRenderer targetSprite = targetPlanet.GetComponent<SpriteRenderer>();
            if (sprite.color != targetSprite.color)
            {
                sprite.color = targetSprite.color;
                targetPlanet.color = sprite.color;
            }
            Destroy(gameObject);
        }

        if (collision.TryGetComponent(out Bullet bullet))
        {
            Destroy(bullet.gameObject);
            StartCoroutine(Destruction());
        }
    }

    #endregion

    protected override void Moving()
    {
        if (isMoving)
        {
            Vector3 newPosition = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
            transform.position = newPosition;
        }
    }

    protected override IEnumerator Destruction()
    {
        Vector2 initialPosition = transform.position;

        if (!isDestruction)
        {
            isRotation = false;
            isMoving = false;

            float randomAngle = Random.Range(0f, 360f);
            Quaternion newRotation = Quaternion.Euler(0f, 0f, randomAngle);
            transform.rotation = newRotation;

            Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            Vector2 newPosition = initialPosition + randomDirection * Random.Range(-0.5f, 0.5f);
            transform.position = newPosition;


            colliderUnit.enabled = false;
            isDestruction = true;
            animator.Play("Bang");

            yield return new WaitForSeconds(0.4f);

            Destroy(gameObject);
        }
    }
}
