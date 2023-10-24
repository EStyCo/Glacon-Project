using System.Collections;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class Cruiser : Ship
{
    [Header("Difference")] 
    public float movementSpeed = 0.55f;
    public int health = 20;

    [Header("Skin Settings")]
    [SerializeField] GameObject gun;
    [SerializeField] GameObject shield;
    public SpriteResolver skinCruiser;
    public SpriteResolver skinShield;


    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != gameObject.tag)
        {
            Cruiser cruiser = collision.gameObject.GetComponent<Cruiser>();

            if (cruiser != null && cruiser.health == health)
            {
                StartCoroutine(Destruction());
                cruiser.StartCoroutine(Destruction());
            }
            else if (cruiser != null && cruiser.health > health)
            {
                cruiser.health -= health;
                StartCoroutine(Destruction());
            }
            else if (cruiser != null && cruiser.health < health)
            {
                health -= cruiser.health;
                cruiser.StartCoroutine(Destruction());
            }
            else
            {
                if (health > 0)
                {
                    health -= 1;
                }
                if (health <= 0)
                {
                    StartCoroutine(Destruction());
                }
            }
        }
    }

    protected override void Moving()
    {
        if (isMoving)
        {
            Vector3 newPosition = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
            transform.position = newPosition;
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
            int tempUnits = targetPlanet.currentUnitCount - health;

            if (tempUnits <= 0)
            {
                targetPlanet.currentUnitCount -= (health + tempUnits);
                ChangeTagPlanet();
                targetPlanet.currentUnitCount += Mathf.Abs(tempUnits);
            }
            else
            {
                targetPlanet.currentUnitCount -= health;
            }
            StartCoroutine(Destruction());
        }
        else if (targetPlanet != null &&
                 collision.gameObject == targetPlanet.gameObject &&
                 gameObject.tag == collision.gameObject.tag)
        {
            targetPlanet.currentUnitCount += health;

            SpriteRenderer targetSprite = targetPlanet.GetComponent<SpriteRenderer>();
            if (sprite.color != targetSprite.color)
            {
                sprite.color = targetSprite.color;
                targetPlanet.color = sprite.color;
            }
            Destroy(gameObject);
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
            transform.rotation = Quaternion.Euler(0f, 0f, randomAngle);

            Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            transform.position = initialPosition + randomDirection * Random.Range(-0.5f, 0.5f);

            colliderUnit.enabled = false;
            gun.SetActive(false);
            shield.SetActive(false);

            isDestruction = true;
            animator.Play("Bang");

            yield return new WaitForSeconds(0.4f);

            Destroy(gameObject);
        }
    }

    public void ShowShield(bool isEnabled)
    {
        if (isEnabled)
            shield.GetComponent<SpriteRenderer>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
        else
            shield.GetComponent<SpriteRenderer>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 0f / 255f);
    }
}
