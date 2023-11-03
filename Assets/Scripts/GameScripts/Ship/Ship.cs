using System.Collections;
using UnityEngine;

public abstract class Ship : MonoBehaviour
{
    [Header("Difference")]
    public float movementSpeed = 0;
    public int armor = 0;
    public int health = 0;
    public float damage = 0;

    [HideInInspector] public Transform target;
    [HideInInspector] public GameObject unitPrefab;
    [HideInInspector] public GameObject cruiserPrefab;
    [HideInInspector] public bool isImmuneToTP = false;
    [HideInInspector] public string tagUnit;
    [HideInInspector]public Planet targetPlanet;

    protected Animator animator;

    protected GameObject canvasParent;
    protected SpriteRenderer sprite;
    protected CapsuleCollider2D colliderUnit;
    protected Rigidbody2D rb;

    protected bool isMoving = true;
    protected bool isDestruction = false;
    protected bool isRotation = true;
    protected bool isAbsorb = false;
    

    protected float suctionForce = 0.55f;

    protected abstract void OnCollisionStay2D(Collision2D collision);

    protected abstract void OnTriggerStay2D(Collider2D collision);

    protected abstract IEnumerator Destruction();

    abstract protected void Moving();

    protected void Start()
    {
        animator = GetComponentInChildren<Animator>();
        canvasParent = GameObject.FindGameObjectWithTag("CanvasParent");
        rb = GetComponent<Rigidbody2D>();
        colliderUnit = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

        tagUnit = gameObject.tag;
        StartCoroutine(CorrectAngleTracking());
    }

    protected void Update() => Moving();

    public void SetTarget(Planet tagetP) => targetPlanet = tagetP;

    public void ImmuneToTP() => StartCoroutine(StartImmune());

    protected IEnumerator CorrectAngleTracking()
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

    protected IEnumerator StartImmune()
    {
        isImmuneToTP = true;

        while (isImmuneToTP)
        {
            yield return new WaitForSeconds(0.75f);
            isImmuneToTP = false;
        }
    }


    protected IEnumerator Absorb(Collider2D collision)
    {
        isAbsorb = true;
        isMoving = false;

        int randomIndex = Random.Range(1, 5);

        for (int i = 0; i < randomIndex; i++)
        {
            Vector3 directionToCenter = (collision.transform.position - transform.position).normalized;
            rb.AddForce(suctionForce * directionToCenter, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.25f);
        }
        float randomChance = Random.value;
        if (randomChance <= 0.2f)
        {
            StartCoroutine(Destruction());
        }
        isMoving = true;
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BlackHole"))
        {
            isAbsorb = false;
        }
    }

    protected void ChangeTagPlanet()
    {
        if (targetPlanet.currentUnitCount < 1)
        {
            targetPlanet.DeselectPlanet();
            targetPlanet.tag = tagUnit;
            targetPlanet.turret.GetComponent<Turret>().ChangeTag(tagUnit);
            targetPlanet.planetRenderer.color = sprite.color;
            targetPlanet.CheckMakeUnits();
            targetPlanet.CheckProgress();

            targetPlanet.unitPrefab = unitPrefab;
            targetPlanet.cruiserPrefab = cruiserPrefab;
            targetPlanet.currentUnitCount = 0;
        }
    }

    protected void DecreaseHealth(int enemyDamage)
    {
        health--;

        if (Random.Range(0, 101) > enemyDamage)
            health--;

        if (health <= 0)
            StartCoroutine(Destruction());
    }
}
