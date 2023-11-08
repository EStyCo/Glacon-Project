using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private Transform turret;
    [SerializeField] private GameObject bullet;
    [SerializeField] private LayerMask contactLayers;
    private ContactFilter2D filter = new ContactFilter2D();
    private CircleCollider2D circleCollider;

    public List<Transform> enemyList = new List<Transform>();
    public Collider2D[] colliders = new Collider2D[50];
    public Transform targetPosition;

    public string tagPlanet;

    public bool isRotateTurret = false;

    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        //turret.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0f, 360f)));
        tagPlanet = transform.parent.gameObject.tag;
        StartCoroutine(CycleTurret());
    }

    private IEnumerator Shooting()
    {
        /*        Transform parent = transform.parent.GetComponent<Planet>().unitsParent.transform;

                while (true)
                {
                    if (targetPosition != Vector2.zero)
                    {
                        Instantiate(bullet, turret.transform.position, turret.transform.rotation, parent);
                        bullet.tag = tagPlanet;
                    }

                    yield return new WaitForSeconds(0.95f);
                }*/
        yield return null;
    }

    public void ChangeTag(string tag)
    {
        tagPlanet = tag;
    }

    private IEnumerator CycleTurret()
    {
        while (true)
        {
            FindTarget();

            yield return new WaitForSeconds(0.2f);
        }
    }

    private void FindTarget()
    {
        enemyList.Clear();

        filter.useLayerMask = true;
        filter.layerMask = contactLayers;

        int numColliders = Physics2D.OverlapCollider(circleCollider, filter, colliders);

        for (int i = 0; i < numColliders; i++)
        {
            if (colliders[i] != null && colliders[i] != circleCollider && colliders[i].gameObject.tag != tagPlanet)
            {
                enemyList.Add(colliders[i].transform);
            }
        }

        CalculateTarget();
    }

    private void CalculateTarget()
    {
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (Transform enemy in enemyList)
        {
            if (enemy == null || ReferenceEquals(enemy, null))
            {
                break;
            }

            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
            targetPosition = closestEnemy;

        if (!isRotateTurret)
        {
            StartCoroutine(AimTarget());
        }
    }

    #region Aim

    private IEnumerator AimTarget()
    {
        isRotateTurret = true;
        Vector2 direction = targetPosition.position - transform.position;
        direction.Normalize();

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(targetAngle - 90f, Vector3.forward);

        while (transform.rotation != rotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }

        isRotateTurret = false;
    }

    #endregion
}
