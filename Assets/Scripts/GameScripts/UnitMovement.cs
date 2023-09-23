using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public Transform target;
    private Planet targetPlanet;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    public string tagUnit;
    private float movementSpeed = 1.15f;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        tagUnit = gameObject.tag;
        StartCoroutine(CorrectAngleTracking());
    }
    public void SetTarget(Planet tagetP)
    {
        targetPlanet = tagetP;
    }
    private void Update()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * movementSpeed * Time.deltaTime;
    }
    private void StartTracking()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * movementSpeed;
    }
    IEnumerator CorrectAngleTracking()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 direction = (target.position - transform.position).normalized;

        while (true)
        {
            if (target != null)
            {
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
                transform.rotation = targetRotation;
                //rb.velocity = direction * movementSpeed;
                //rb.AddForce(direction * movementSpeed);
                //transform.Translate(Vector3.forward.);

                if (Vector3.Distance(transform.position, target.position) < 0.6f)
                {
                    ChangeTagPlanet();
                    Destroy(gameObject);
                    SoundManager.Instance.PlayNoise();
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != gameObject.tag)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (targetPlanet != null && Vector3.Distance(transform.position, target.position) < 0.7f && gameObject.tag != targetPlanet.tag)
        { 
            targetPlanet.DecreaseUnits();
        }
        else if (targetPlanet != null && Vector3.Distance(transform.position, target.position) < 0.7f && gameObject.tag == targetPlanet.tag)
        {
            targetPlanet.IncreaseUnits();

            SpriteRenderer targetSprite = targetPlanet.GetComponent<SpriteRenderer>();
            if (sprite.color != targetSprite.color)
            { 
                sprite.color = targetSprite.color;
                targetPlanet.originalColor = sprite.color; 
            }
        }
    }
    private void ChangeTagPlanet()
    {
        if (targetPlanet.currentUnitCount < 1)
        {
            targetPlanet.tag = tagUnit;
            targetPlanet.planetRenderer.color = sprite.color;
            targetPlanet.CheckMakeUnits();
        }
    }
    private bool CheckDistance()
    {
        if (Vector3.Distance(transform.position, target.position) < 0.8f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
