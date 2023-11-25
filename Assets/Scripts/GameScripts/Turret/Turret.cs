using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Turret : MonoBehaviour
{
    [Inject] private ShipConstructor shipConstructor;
    [Inject] protected DiContainer diContainer;
    [Inject(Id = "Bullet")] private GameObject bullet;

    [HideInInspector] public string tagPlanet;
    [HideInInspector] public List<Vector3> listEnemy = new List<Vector3>();

    [SerializeField] private LayerMask objectLayer;
    [SerializeField] private Transform turret;

    private Transform parent;
    private Vector3 target;

    #region COLLISIONS

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ship ship) && !listEnemy.Contains(ship.transform.position) && !ship.CompareTag(tagPlanet))
        {
            listEnemy.Add(ship.GetPosition().position);
        }
    } 

    #endregion

    #region MONO

    private void Start()
    {
        gameObject.transform.parent.TryGetComponent(out Planet planet);
        gameObject.transform.parent.TryGetComponent(out Cruiser cruiser);
        parent = planet?.unitsParent.transform ?? cruiser.transform.parent;

        turret.transform.rotation = Quaternion.Euler(new Vector3(0, 0, UnityEngine.Random.Range(0f, 360f)));
        tagPlanet = transform.parent.gameObject.tag;
        StartCycleTurret();
    }

    #endregion

    public void ChangeTag(string tag)
    {
        tagPlanet = tag;

        StopAllCoroutines();
        StartCycleTurret();
    }

    private void StartCycleTurret()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(ClearList());
            StartCoroutine(FindNearTarget());
            StartCoroutine(Shooting());
            StartCoroutine(AimTarget());
        }
    }

    private IEnumerator FindNearTarget()
    {
        while (true)
        {
            while (listEnemy.Count > 0)
            {
                float closestDistance = Mathf.Infinity;

                foreach (Vector3 ship in listEnemy)
                {
                    float distance = Vector3.Distance(transform.position, ship);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        target = ship;
                    }
                }

                yield return new WaitForSeconds(0.05f);
            }

            target = Vector3.zero;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator ClearList()
    {
        while (true)
        {
            listEnemy.Clear();
            target = Vector3.zero;

            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator AimTarget()
    {
        while (true)
        {
            while (target != Vector3.zero)
            {
                yield return new WaitForSeconds(0.04f);
                CorrectAngle();
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void CorrectAngle()
    {
        if (target != Vector3.zero)
        {
            Vector3 dir = target - transform.position;
            dir.Normalize();
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            turret.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }
    }

    private IEnumerator Shooting()
    {
        while (true)
        {
            if (target != Vector3.zero)
            {
                GameObject bulletInstance = diContainer.InstantiatePrefab(bullet, turret.transform.position, turret.transform.rotation, parent);
                bulletInstance.tag = tagPlanet;
            }

            yield return new WaitForSeconds(shipConstructor.reloadSpeed);
        }
    }
}