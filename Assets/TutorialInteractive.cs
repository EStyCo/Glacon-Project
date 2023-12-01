using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialInteractive : MonoBehaviour
{
    [SerializeField] private Tutorial tutorial;
    [SerializeField] private CosmoCat cosmoCat;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private GameObject block;

    [Header("Start Dialog")]
    [SerializeField] private string startingDialog;

    [Header("GeneralRules")]
    [SerializeField] private string generalRules;

    [Header("Management")]
    [SerializeField] private string management;

    [Header("Selector")]
    [SerializeField] private string selector;
    [SerializeField] private SpriteRenderer selectorSprite;

    [Header("BalancePower")]
    [SerializeField] private string balancePower;
    [SerializeField] private SpriteRenderer balancePowerSprite;

    [Header("Unit")]
    [SerializeField] private string unit;

    [Header("Cruiser")]
    [SerializeField] private string cruiser;

    [Header("Progress")]
    [SerializeField] private string progress;

    private float speedText = 0.055f;
    private bool isBlink = false;
    private bool fadingOut = true;

    private void Start()
    {
        cosmoCat.ShowCC();

        StartCoroutine(Dialog(startingDialog, true));
    }

    private IEnumerator Dialog(string str, bool withBlock)
    {
        dialogText.text = "";

        if (withBlock) block.SetActive(true);
        yield return new WaitForSeconds(1f);

        cosmoCat.TalkCC();

        foreach (char c in str)
        {
            dialogText.text += c;
            yield return new WaitForSeconds(speedText);
        }

        cosmoCat.IdleCC();
        block.SetActive(false);
    }

    private IEnumerator Blink(SpriteRenderer sprite)
    {
        if (isBlink) yield break;

        float speedBlink = 0.01f;

        while (true)
        {
            Color currentColor = sprite.color;
            float newAlpha = currentColor.a;

            if (fadingOut)
            {
                newAlpha -= speedBlink;
                if (newAlpha <= 0.35f)
                {
                    fadingOut = false;
                    yield return new WaitForSeconds(0.15f);
                }
            }
            else
            {
                newAlpha += speedBlink;
                if (newAlpha >= 1f)
                {
                    fadingOut = true;
                    yield return new WaitForSeconds(0.15f);
                }
            }

            sprite.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);

            yield return null;
        }

    }

    public void GeneralRules()
    {
        StopAllCoroutines();
        tutorial.NewGeneration();
        StartCoroutine(Dialog(generalRules, false));
    }

    public void Management()
    {
        StopAllCoroutines();
        tutorial.NewGeneration();
        StartCoroutine(Dialog(management, false));
    }

    public void Selector()
    {
        StopAllCoroutines();
        tutorial.NewGeneration();
        StartCoroutine(Dialog(selector, false));
        StartCoroutine(Blink(selectorSprite));
    }

    public void BalancePower()
    {
        StopAllCoroutines();
        tutorial.NewGeneration();
        StartCoroutine(Dialog(balancePower, false));
        StartCoroutine(Blink(balancePowerSprite));
    }

    public void Unit()
    {
        StopAllCoroutines();
        tutorial.NewGeneration();
        StartCoroutine(Dialog(unit, false));
    }

    public void Cruiser()
    {
        StopAllCoroutines();
        tutorial.NewGeneration();
        StartCoroutine(Dialog(cruiser, false));
    }

    public void Progress()
    {
        StopAllCoroutines();
        tutorial.NewGeneration();
        StartCoroutine(Dialog(progress, false));
    }
}
