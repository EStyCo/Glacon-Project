using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform turret;
    [SerializeField] private GameObject bullet;

    public Transform target;
    public string tagPlanet;

    public bool isHasTarget = false;

    private void Start()
    {
        turret.transform.rotation = Quaternion.Euler(new Vector3(0, 0, UnityEngine.Random.Range(0f, 360f)));
        tagPlanet = transform.parent.gameObject.tag;
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        while (true)
        {
            if (target != null)
            {
                GameObject bulletInstance = Instantiate(bullet, turret.transform.position, turret.transform.rotation, turret);
                bullet.tag = tagPlanet;
            }

            yield return new WaitForSeconds(0.75f);
        }
    }

    public void ChangeTag(string tag)
    {
        tagPlanet = tag;

        StopAllCoroutines();
        isHasTarget = false;
    }

    #region Triggers and Collisions

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != tagPlanet && !isHasTarget)
        {
            target = collision.transform;
            isHasTarget = true;

            if (gameObject.activeSelf)
            {
                StartCoroutine(AimTarget());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform == target)
        {
            target = null;
            isHasTarget = false;
        }
    }

    #endregion

    #region Aim

    private IEnumerator AimTarget()
    {
        while (target != null || isHasTarget)
        {
            if (target.IsDestroyed())
            {
                isHasTarget = false;
            }

            yield return new WaitForSeconds(0.04f);
            CorrectAngle();
        }

        isHasTarget = false;
    }

    private void CorrectAngle()
    {
        if (target != null)
        {
            Vector3 dir = target.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            turret.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }
    }

    #endregion

}
