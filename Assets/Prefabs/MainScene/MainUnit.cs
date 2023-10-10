using System.Collections;
using UnityEngine;

public class MainUnit : MonoBehaviour
{
    private GameObject canvasParent;
    public Transform target;
    public GameObject unitPrefab;
    private MainPlanet targetPlanet;
    private SpriteRenderer sprite;
    private Animator animator;
    private CapsuleCollider2D colliderUnit;

    private bool isMoving = true;
    private bool isDestruction = false;
    private bool isRotation = true;
    public string tagUnit;
    private float movementSpeed = 1.15f;

    private void Start()
    {
        canvasParent = GameObject.FindGameObjectWithTag("CanvasParent");
        if (canvasParent == null) Debug.LogWarning("Не найден родитель канвас для планет! Юнит");

        colliderUnit = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        tagUnit = gameObject.tag;
        StartCoroutine(CorrectAngleTracking());
    }
    private void Update()
    {
        Moving();
    }
    public void SetTarget(MainPlanet tagetP)
    {
        targetPlanet = tagetP;
    }
    private void Moving()
    {
        if (isMoving)
        {
            Vector3 newPosition = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
            transform.position = newPosition;
        }
    }
    IEnumerator CorrectAngleTracking()
    {
        while (isRotation)
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != gameObject.tag)
        {
            StartCoroutine(Destruction());
        }
    }
    IEnumerator Destruction()
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (targetPlanet != null &&
            collision.gameObject == targetPlanet.gameObject &&
            gameObject.tag != collision.gameObject.tag)
        {
            StartCoroutine(Destruction());
        }
    }
}
