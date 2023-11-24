using System.Collections;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class Cruiser : Ship
{
    [Header("Skin Settings")]
    [SerializeField] GameObject turret;
    [SerializeField] GameObject shield;
    [SerializeField] GameObject airCraftSpawner;

    public SpriteResolver skinCruiser;
    public SpriteResolver skinShield;

    public bool isGrowthingCruiser = false;
    public bool isCollision = false;
    private bool hasAnimDestr = false;

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag != gameObject.tag)
        {
            if (collision.gameObject.TryGetComponent(out Cruiser cruiser) && gameObject.GetComponent<Cruiser>() != null)
            {
                CollisionCruiser(cruiser, collision);
            }

            if (collision.gameObject.TryGetComponent(out Unit unit))
            {
                AvoidCollision(1);
                Physics2D.IgnoreCollision(colliderUnit, collision.collider);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag(collision.gameObject.tag)) StartCoroutine(IgnoreCollision(collision.gameObject, gameObject));
    }

    private void CollisionShield(ShieldPlanet shield, Collider2D collision)
    {
        if (!isCollision)
        {
            int enemyHealt = shield.health;
            int mainHealth = health;

            AvoidCollision(enemyHealt);
            shield.DecreasedHealth(mainHealth);
        }

        isCollision = true;
        shield.isCollision = true;

        Physics2D.IgnoreCollision(colliderUnit, collision);
    }

    private void CollisionCruiser(Cruiser cruiser, Collision2D collision)
    {
        if (!isCollision)
        {
            int enemyHealt = cruiser.health;
            int mainHealth = health;

            AvoidCollision(enemyHealt);
            cruiser.AvoidCollision(mainHealth);
        }

        isCollision = true;
        cruiser.isCollision = true;

        Physics2D.IgnoreCollision(colliderUnit, collision.collider);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isCollision = false;

        if (!collision.gameObject.CompareTag(gameObject.tag))
            Physics2D.IgnoreCollision(colliderUnit, collision.collider, false);
    }

    public void AvoidCollision(int count)
    {
        int tempArmor = armor -= count;

        if (tempArmor <= 0)
        {
            health -= Mathf.Abs(tempArmor);
            armor = 0;
        }

        if (health <= 0)
        {
            StartCoroutine(Destruction());
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

        if (collision.gameObject.TryGetComponent(out ShieldPlanet shield) && !gameObject.CompareTag(collision.gameObject.tag))
        {
            CollisionShield(shield, collision);
        }

        if (targetPlanet != null &&
            collision.gameObject == targetPlanet.gameObject &&
            gameObject.tag != collision.gameObject.tag)
        {
            DesantToPlanet(collision.gameObject);
        }

        else if (targetPlanet != null &&
                 collision.gameObject == targetPlanet.gameObject &&
                 collision.gameObject.CompareTag(tagUnit))
        {
            if (!isGrowthingCruiser)
                targetPlanet.currentUnitCount += health;

            if (targetPlanet.currentUnitCount > targetPlanet.maxUnitCurrent)
                targetPlanet.currentUnitCount = targetPlanet.maxUnitCurrent;

            CheckColor();

            if (hasAnimDestr)
                StartCoroutine(Destruction());
            else
                Destroy(gameObject);

            //ChangeTagPlanet();
        }

        if (collision.TryGetComponent(out Bullet bullet) && bullet.tag != tag)
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
            planet.currentUnitCount -= (1 + (damage - planet.armor));
            health--;

            if (health <= 0)
                StartCoroutine(Destruction());
            if (planet.currentUnitCount < 1)
                break;
        }


        ChangeTagPlanet();
        hasAnimDestr = true;
    }

    protected override IEnumerator Destruction()
    {
        Vector2 initialPosition = transform.position;

        if (!isDestruction)
        {
            isRotation = false;
            isMoving = false;

            sprite.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0 / 255f);
            float randomAngle = Random.Range(0f, 360f);
            transform.rotation = Quaternion.Euler(0f, 0f, randomAngle);

            Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            transform.position = initialPosition + randomDirection * Random.Range(-0.25f, 0.25f);

            colliderUnit.enabled = false;
            turret.SetActive(false);
            shield.SetActive(false);

            isDestruction = true;
            animator.Play("BangCruiser");

            yield return new WaitForSeconds(0.55f);

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

    public void OnTurret()
    {
        turret.SetActive(true);
    }

    public void OnAirCraftSpawner()
    {
        airCraftSpawner.SetActive(true);
    }

    public override void SetMoveSpeed()
    {
        movementSpeed = constructor.speedCruisers;
    }

    private void OnDestroy()
    {
        balancePower.GetFlyingShips(originalHealth, gameObject.tag, false);

        if (isGrowthingCruiser)
            growth.DisableFlag(gameObject.tag);
    }
}
