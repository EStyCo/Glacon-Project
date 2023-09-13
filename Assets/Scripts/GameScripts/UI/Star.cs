using UnityEngine;
using System.Collections;
using UnityEngine.U2D.Animation;

public class Star : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private SpriteResolver spriteResolver;
    private bool fadingOut = true;
    private float startDelay;
    private float startAlpha; // Ќачальна€ прозрачность

    private void Awake()
    {
        startDelay = Random.Range(0.5f, 3.1f);
        startAlpha = Random.Range(0.3f, 1.0f); // »нициализируем начальную прозрачность случайным образом
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteResolver = GetComponent<SpriteResolver>();

        ChoiceStar();

        StartCoroutine(StarFadingCoroutine());
    }

    private void ChoiceStar()
    {
        spriteResolver.SetCategoryAndLabel("Stars", "Star" + Random.Range(1, 6));
    }

    private IEnumerator StarFadingCoroutine()
    {
        yield return new WaitForSeconds(startDelay);

        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, startAlpha);

        while (true)
        {
            Color currentColor = spriteRenderer.color;
            float newAlpha = currentColor.a;

            if (fadingOut)
            {
                newAlpha -= Random.Range(0.0003f, 0.15f) * Time.deltaTime;
                if (newAlpha <= 0)
                {
                    fadingOut = false;
                    yield return new WaitForSeconds(Random.Range(0.6f, 2.25f));
                }
            }
            else
            {
                newAlpha += Random.Range(0.025f, 0.35f) * Time.deltaTime;
                if (newAlpha >= 0.6f)
                {
                    fadingOut = true;
                    yield return new WaitForSeconds(Random.Range(0.6f, 2.25f));
                }
            }

            spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);

            yield return null;
        }
    }
}
