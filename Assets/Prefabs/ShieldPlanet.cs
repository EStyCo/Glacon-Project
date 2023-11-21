using System.Collections;
using UnityEngine;
using Zenject;

public class ShieldPlanet : MonoBehaviour
{
    [Inject] ShipConstructor constructor;

    public int health;
    private SpriteRenderer spriteRenderer;
    private Planet planet;
    private CapsuleCollider2D colliderShield;
    public Transform center;

    private float angle;
    private float bustRadius;

    public bool isCollision = false;

    private void Start()
    {
        colliderShield = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        planet = transform.parent.GetComponent<Planet>();

        ResetShield();
    }

    void Update()
    {
        Moving();
    }

    private void Moving()
    {
        angle -= constructor.speedShield * Time.deltaTime;

        float x = center.position.x + (constructor.radiusShield + bustRadius) * Mathf.Cos(angle);
        float y = center.position.y + (constructor.radiusShield + bustRadius) * Mathf.Sin(angle);

        transform.position = new Vector3(x, y, 0);

        transform.right = -(transform.position - center.position).normalized;
    }

    public void ResetShield()
    {
        colliderShield = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        planet = transform.parent.GetComponent<Planet>();

        SetRadius();
        angle = Random.Range(0f, 50f);

        spriteRenderer.color = planet.GetComponent<SpriteRenderer>().color;

        if (planet.unitPrefab != null)
        {
            spriteRenderer.sprite = planet.unitPrefab.GetComponent<SpriteRenderer>().sprite;
            gameObject.layer = planet.cruiserPrefab.layer;
        }

        health = constructor.healthShield;
        gameObject.tag = planet.gameObject.tag;
    }

    private void SetRadius()
    {
        switch (planet.selectedSize)
        {
            case Planet.Size.medium:
                bustRadius = 0.10f;
                break;

            case Planet.Size.large:
                bustRadius = 0.25f;
                break;

            default:
                break;
        }
    }

    public void DecreasedHealth(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine(RestTimer());
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Cruiser cruiser) && gameObject.GetComponent<Cruiser>() != null)
        {
            if (!isCollision)
            {
                int enemyHealt = cruiser.health;
                int mainHealth = health;

                DecreasedHealth(enemyHealt);
                cruiser.AvoidCollision(mainHealth);
            }

            isCollision = true;
            cruiser.isCollision = true;
        }

        if (collision.gameObject.TryGetComponent(out Unit unit))
        {
            DecreasedHealth(1);
        }
    }

    private IEnumerator RestTimer()
    {
        colliderShield.enabled = false;
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);

        yield return new WaitForSeconds(constructor.restTimerShield);

        ResetShield();
    }
}