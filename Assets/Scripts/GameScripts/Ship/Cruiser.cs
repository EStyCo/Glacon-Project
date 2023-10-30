using System.Collections;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class Cruiser : Ship
{
    [Header("Skin Settings")]
    [SerializeField] GameObject gun;
    [SerializeField] GameObject shield;
    public SpriteResolver skinCruiser;
    public SpriteResolver skinShield;


    protected override void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag != gameObject.tag)
        {
            if (collision.gameObject.TryGetComponent(out Cruiser cruiser) && gameObject.GetComponent<Cruiser>() != null)
            {
                isTryAvoid = true;

                int enemyDamage = cruiser.damage;
                int enemyHealth = cruiser.health;
                int mainHealth = health;

                if (enemyHealth <= 0 && mainHealth <= 0)
                    return;

                if (cruiser.isTryAvoid)
                {
                    TryAvoidCollision(enemyHealth, damage);
                    cruiser.TryAvoidCollision(mainHealth, enemyDamage);
                }
            }

            if (collision.gameObject.TryGetComponent(out Unit unit))
            {
                int enemyDamage = unit.damage;

                TryAvoidCollision(unit.health, enemyDamage);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isTryAvoid = false;
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
            DesantToPlanet(collision.gameObject);
        }
        else if (targetPlanet != null &&
                 collision.gameObject == targetPlanet.gameObject &&
                 gameObject.tag == collision.gameObject.tag)
        {
            targetPlanet.currentUnitCount += health;

            if (targetPlanet.currentUnitCount > targetPlanet.maxUnitCurrent)
                targetPlanet.currentUnitCount = targetPlanet.maxUnitCurrent;

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
            health--;

            if (health <= 0)
                StartCoroutine(Destruction());
        }
    }

    private void DesantToPlanet(GameObject obj)
    {
        Planet planet = obj.GetComponent<Planet>();
        int index = health;

        for (int i = 0; i < index; i++)
        {
            if (Random.Range(0, 101) < damage)
            {
                planet.DecreaseUnits();
                health--;
            }

            if (Random.Range(0, 101) < planet.armor)
            {
                health--;
            }
            else
            {
                planet.DecreaseUnits();
                health--;
            }


            if (planet.currentUnitCount <= 0)
            {
                ChangeTagPlanet();
                planet.currentUnitCount += (index - i);
                break;
            }
        }

        StartCoroutine(Destruction());
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
