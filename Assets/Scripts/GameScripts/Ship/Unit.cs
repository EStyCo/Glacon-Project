using System.Collections;
using UnityEngine;

public class Unit : Ship
{
    public override void SetMoveSpeed()
    {
        movementSpeed = constructor.speedUnits;
    }

    #region COLLISIONS
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag(collision.gameObject.tag)) StartCoroutine(IgnoreCollision(collision.gameObject, gameObject));
    }

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag != gameObject.tag)
        {
            Physics2D.IgnoreCollision(colliderUnit, collision.collider);
            AvoidCollision();
        }
    }

    private void OnDestroy()
    {
        balancePower.GetFlyingShips(originalHealth, gameObject.tag, false);
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


        if (collision.gameObject.TryGetComponent(out ShieldPlanet shield) && !gameObject.CompareTag(shield.gameObject.tag))
        {
            StartCoroutine(Destruction());
        }

        if (targetPlanet != null &&
            collision.gameObject == targetPlanet.gameObject &&
            gameObject.tag != collision.gameObject.tag)
        {
            Planet planet = collision.gameObject.GetComponent<Planet>();

            targetPlanet.currentUnitCount -= (health + (damage - planet.armor));
            ChangeTagPlanet();
            StartCoroutine(Destruction());
        }
        else if (targetPlanet != null &&
                 collision.gameObject == targetPlanet.gameObject &&
                 gameObject.tag == collision.gameObject.tag)
        {
            targetPlanet.IncreaseUnits();

            CheckColor();
            Destroy(gameObject);
        }

        if (collision.TryGetComponent(out Bullet bullet) && bullet.tag != tag)
        {
            Destroy(bullet.gameObject);
            StartCoroutine(Destruction());
        }
    }

    #endregion

    private void AvoidCollision()
    {
        armor--;

        if (armor < 0)
        {
            StartCoroutine(Destruction());
        }
    }

    protected override void Moving()
    {
        if (isMoving)
        {
            Vector3 newPosition = Vector2.MoveTowards(transform.position, target.position + blurTarget, movementSpeed * Time.deltaTime);
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
            sprite.sortingOrder = -1;
            animator.Play("Bang2");

            yield return new WaitForSeconds(0.55f);

            Destroy(gameObject);
        }
    }
}
