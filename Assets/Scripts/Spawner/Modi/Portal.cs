using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform targetPortal;
    [SerializeField] private Portals portals;
    public event Action<Portal> OnDestroyEvent;

    private float countTimer;

    void Start()
    {
        portals = FindFirstObjectByType<Portals>();

        countTimer = UnityEngine.Random.Range(10, 30);
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        while (countTimer > 0)
        {
            yield return new WaitForSeconds(1f);
            countTimer--;
        }

        // Вызываем событие при уничтожении портала
        OnDestroyEvent?.Invoke(this);

        Debug.Log("Портал уничтожился!");

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var unit = collision.GetComponent<Unit>();

        if (unit != null && !unit.isImmuneToTP)
        {
            // Создаем список порталов без текущего портала
            List<Portal> otherPortals = new List<Portal>();
            foreach (var portalObject in portals.listPortals)
            {
                Portal otherPortal = portalObject.GetComponent<Portal>();
                if (otherPortal != this) // Исключаем текущий портал из списка
                {
                    otherPortals.Add(otherPortal);
                }
            }

            if (otherPortals.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, otherPortals.Count);
                var newPortal = otherPortals[randomIndex];

                if (newPortal != null)
                {
                    unit.ImmuneToTP();
                    unit.transform.position = newPortal.transform.position;
                }
            }
        }
    }
}
