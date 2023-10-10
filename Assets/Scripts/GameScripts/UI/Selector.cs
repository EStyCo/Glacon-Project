using System.Collections;
using UnityEngine;

public class Selector : MonoBehaviour
{
    public enum Percent
    {
        _20 = 20,
        _40 = 40,
        _60 = 60,
        _80 = 80,
        _100 = 100
    }
    public static Selector Instance { get; private set; }

    private Animator selectorAnimator;
    public Percent selectedPercent { get; private set; }
    private bool isSwitch = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        selectedPercent = Percent._40;
        selectorAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        InputSelector();
    }
    private void InputSelector()
    {
        bool pressedTop = Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.W);
        bool pressedBottom = Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.S);
        float scrollDelta = Input.mouseScrollDelta.y;

        if ((scrollDelta > 0 || pressedTop) && (int)selectedPercent <= 80 && !isSwitch)
        {
            Percent newSelectedPercent = (Percent)((int)selectedPercent + 20);
            StartCoroutine(SwitchSelector(newSelectedPercent, false));
        }
        else if ((scrollDelta < 0 || pressedBottom) && (int)selectedPercent >= 40 && !isSwitch)
        {
            Percent newSelectedPercent = (Percent)((int)selectedPercent - 20);
            StartCoroutine(SwitchSelector(newSelectedPercent, true));
        }
    }
    private IEnumerator SwitchSelector(Percent newPercent, bool isInverse)
    {
        if (!isSwitch && !isInverse)
        {
            isSwitch = true;

            string oldIndex = selectedPercent.ToString().Substring(1);
            string newIndex = newPercent.ToString().Substring(1);
            string animIndex = oldIndex + '-' + newIndex;

            selectorAnimator.Play(animIndex);

            yield return new WaitForSeconds(0.05f);

            selectedPercent = newPercent;
        }
        else if (!isSwitch && isInverse)
        {
            isSwitch = true;

            string oldIndex = selectedPercent.ToString().Substring(1);
            string newIndex = newPercent.ToString().Substring(1);
            string animIndex = oldIndex + '-' + newIndex;

            selectorAnimator.Play(animIndex);

            yield return new WaitForSeconds(0.05f);

            selectedPercent = newPercent;
        }
        isSwitch = false;
    }
}
