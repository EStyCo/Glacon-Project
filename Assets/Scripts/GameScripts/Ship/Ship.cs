using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour
{
    protected GameObject canvasParent;
    public Transform target;
    public GameObject unitPrefab;
    public GameObject cruiserPrefab;
    protected Planet targetPlanet;
    protected SpriteRenderer sprite;
    protected Animator animator;
    protected CapsuleCollider2D colliderUnit;
    protected Rigidbody2D rb;

    public bool isImmuneToTP = false;
    protected bool isMoving = true;
    protected bool isDestruction = false;
    protected bool isRotation = true;
    protected bool isAbsorb = false;
    public string tagUnit;

    protected float suctionForce = 0.55f;

    protected void Start()
    {
        canvasParent = GameObject.FindGameObjectWithTag("CanvasParent");
        rb = GetComponent<Rigidbody2D>();
        colliderUnit = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        tagUnit = gameObject.tag;
        StartCoroutine(CorrectAngleTracking());
    }
    protected void Update()
    {
        Moving();
    }
    public void SetTarget(Planet tagetP)
    {
        targetPlanet = tagetP;
    }
    abstract protected void Moving();

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

    public void ImmuneToTP()
    {
        StartCoroutine(StartImmune());
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

    protected abstract void OnCollisionEnter2D(Collision2D collision);

    protected abstract void OnTriggerStay2D(Collider2D collision);

    protected abstract IEnumerator Destruction();


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
            targetPlanet.tag = tagUnit;
            targetPlanet.planetRenderer.color = sprite.color;
            targetPlanet.CheckMakeUnits();
            targetPlanet.CheckProgress();

            targetPlanet.unitPrefab = unitPrefab;
            targetPlanet.cruiserPrefab = cruiserPrefab;
        }
    }
}
