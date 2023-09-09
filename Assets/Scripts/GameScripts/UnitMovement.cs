using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    private Transform target;
    private float movementSpeed = 1.15f;

    private Planet targetPlanet;

    public void SetTargetPlanet(Planet planet)
    {
        targetPlanet = planet;
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, directionToTarget);
            transform.rotation = targetRotation;
            transform.position += directionToTarget * movementSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, target.position) < 0.6f) Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag("PlayerUnit"))
        {
            if (collision.gameObject.CompareTag("EnemyUnit"))
            {
                Destroy(gameObject);
                BalancePower.Instance.ChangePlayerPower(false);
            }
        }
        else if (gameObject.CompareTag("EnemyUnit"))
        { 
            if (collision.gameObject.CompareTag("PlayerUnit"))
            {
                Destroy(gameObject);
                BalancePower.Instance.ChangeEnemyPower(false);
            }
        }
    }
    private void OnDestroy()
    {
        if (targetPlanet != null && gameObject.CompareTag("PlayerUnit"))
        {
            if (targetPlanet != null && targetPlanet.tag == "PlayerPlanet" && Vector3.Distance(transform.position, target.position) < 0.7f)
            {
                targetPlanet.IncreaseUnits();
            }
            else if (targetPlanet != null && Vector3.Distance(transform.position, target.position) < 0.7f && (targetPlanet.tag == "NeutralPlanet" || targetPlanet.tag == "EnemyPlanet"))
            {
                targetPlanet.DecreaseUnits();

                if (targetPlanet.tag == "EnemyPlanet") BalancePower.Instance.ChangeEnemyPower(false);
                
                BalancePower.Instance.ChangePlayerPower(false);
            }
        }
        else if (targetPlanet != null && gameObject.CompareTag("EnemyUnit"))
        {
            if (targetPlanet != null && targetPlanet.tag == "EnemyPlanet" && Vector3.Distance(transform.position, target.position) < 0.7f)
            {
                targetPlanet.IncreaseUnitsFromEnemy();
            }
            else if (targetPlanet != null && Vector3.Distance(transform.position, target.position) < 0.7f && (targetPlanet.tag == "NeutralPlanet" || targetPlanet.tag == "PlayerPlanet"))
            {
                targetPlanet.DecreaseUnitsFromEnemy();

                if (targetPlanet.tag == "PlayerPlanet") BalancePower.Instance.ChangePlayerPower(false);

                BalancePower.Instance.ChangeEnemyPower(false);
            }
        }
    }
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
