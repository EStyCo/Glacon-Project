using System.Collections;
using UnityEngine;
using Zenject;

public abstract class Ship : MonoBehaviour
{
    [Header("Difference")]
    public int armor = 0;
    public int health = 0;
    public float damage = 0;

    [HideInInspector] public Transform target;
    [HideInInspector] public GameObject unitPrefab;
    [HideInInspector] public GameObject cruiserPrefab;
    [HideInInspector] public bool isImmuneToTP = false;
    [HideInInspector] public string tagUnit;
    [HideInInspector] public Planet targetPlanet;
    [HideInInspector] public Color mainColor;
    [Inject] protected BalancePower balancePower;
    [Inject] protected ShipConstructor constructor;
    [Inject] protected Growth growth;

    protected Animator animator;
    protected GameObject canvasParent;
    protected SpriteRenderer sprite;
    protected CapsuleCollider2D colliderUnit;
    protected Rigidbody2D rb;
    protected Vector3 blurTarget;

    public float movementSpeed = 0;
    protected bool isMoving = true;
    protected bool isDestruction = false;
    protected bool isRotation = true;
    protected bool isAbsorb = false;
    protected int originalHealth;
    protected float suctionForce = 0.55f;

    protected abstract void OnCollisionStay2D(Collision2D collision);

    protected abstract void OnTriggerStay2D(Collider2D collision);

    protected abstract IEnumerator Destruction();

    public abstract void SetMoveSpeed();

    abstract protected void Moving();

    protected void Start()
    {
        animator = GetComponentInChildren<Animator>();
        canvasParent = GameObject.FindGameObjectWithTag("CanvasParent");
        rb = GetComponent<Rigidbody2D>();
        colliderUnit = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        mainColor = sprite.color;

        originalHealth = health;

        balancePower.GetFlyingShips(originalHealth, tagUnit, true);
        balancePower.GetShips(gameObject);

        tagUnit = gameObject.tag;
        //SetMoveSpeed();
        StartCoroutine(CorrectAngleTracking());
        blurTarget = GetBlur();
    }

    protected void Update() => Moving();

    public void SetTarget(Planet tagetP)
    {
        targetPlanet = tagetP;
    }

    private Vector3 GetBlur()
    {
        float xBlur;
        float yBlur;
        do
        {
            xBlur = Random.Range(-0.35f, 0.35f);
            yBlur = Random.Range(-0.35f, 0.35f);
        }
        while (Mathf.Abs(xBlur) <= 0.1f && Mathf.Abs(yBlur) <= 0.1f);

        return new Vector3(xBlur, yBlur, 0);
    }

    public void ImmuneToTP() => StartCoroutine(StartImmune());

    protected IEnumerator CorrectAngleTracking()
    {
        while (isRotation)
        {
            if (target != null)
            {
                Vector3 direction = ((target.position + blurTarget) - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);

                if (Quaternion.Angle(transform.rotation, targetRotation) > 2f)
                {
                    transform.rotation = targetRotation;
                }
            }

            yield return new WaitForSeconds(0.05f);
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

    public Transform GetPosition()
    {
        return gameObject.transform;
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
            //targetPlanet.planetRenderer.color = sprite.color;
            targetPlanet.planetRenderer.color = mainColor;
            targetPlanet.CheckMakeUnits();

            targetPlanet.unitPrefab = unitPrefab;
            targetPlanet.cruiserPrefab = cruiserPrefab;
            targetPlanet.currentUnitCount = 0;
            targetPlanet.CheckProgress();
        }
    }

    protected void CheckColor()
    {
        targetPlanet.TryGetComponent(out SpriteRenderer targetSprite);
        if (mainColor != targetSprite.color)
        {
            targetPlanet.color = mainColor;
            targetSprite.color = mainColor;
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

    protected IEnumerator IgnoreCollision(GameObject obj1, GameObject obj2)
    {
        yield return new WaitForSeconds(0.4f);

        if (obj1 != null && obj2 != null)
        {
            obj1.TryGetComponent(out Collider2D colliderObj1);
            obj2.TryGetComponent(out Collider2D colliderObj2);

            if (colliderObj1 != null && colliderObj2 != null)
            {
                Physics2D.IgnoreCollision(colliderObj1, colliderObj2);
            }
        }
    }
}
