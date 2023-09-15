using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public Animator menuAnimator;

    public GameObject UiMenu;
    public Button buttonPause;

    public void ShowPauseMenu()
    {
        menuAnimator.Play("ShowPM");
        buttonPause.enabled = false;
        StartCoroutine(ShowUiMenu());
    }
    IEnumerator ShowUiMenu() 
    {
        yield return new WaitForSeconds(0.5f);
        UiMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        menuAnimator.Play("HidePM");
    }
}
