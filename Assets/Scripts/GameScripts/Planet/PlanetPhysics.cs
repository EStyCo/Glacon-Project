using System.Collections;
using UnityEngine;

public class PlanetPhysics : MonoBehaviour
{
    private float bounceHeight = 0.25f;
    private float bounceDuration = 10.8f;

    private Vector3 originalPosition;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        originalPosition = transform.position;
        StartCoroutine(StartBounceWithRandomDelay());
    }

    private IEnumerator StartBounceWithRandomDelay()
    {
        float randomDelay = Random.Range(0f, 1.5f);
        yield return new WaitForSeconds(randomDelay);

        while (true)
        {
            float elapsedTime = 0f;

            while (elapsedTime < bounceDuration / 2)
            {
                float newY = Mathf.Lerp(originalPosition.y, originalPosition.y + bounceHeight, elapsedTime / (bounceDuration / 2));
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            while (elapsedTime < bounceDuration)
            {
                float newY = Mathf.Lerp(originalPosition.y + bounceHeight, originalPosition.y, (elapsedTime - (bounceDuration / 2)) / (bounceDuration / 2));
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            yield return null;
        }
    }
}
