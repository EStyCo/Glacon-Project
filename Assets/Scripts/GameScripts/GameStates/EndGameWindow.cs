using TMPro;
using UnityEngine;

public class EndGameWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textEndGame;
    [SerializeField] private TextMeshProUGUI titleEndGame;

    [Header("Buttons")]
    [SerializeField] private GameObject resetButton;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private GameObject mainMenuButton;
    [SerializeField] private GameObject returnButton;

    [Header("Strings")]
    [TextArea][SerializeField] private string campaignWinText;
    [TextArea][SerializeField] private string campaignLoseText;

    [TextArea][SerializeField] private string sandboxWinText;
    [TextArea][SerializeField] private string sandboxLoseText;

    public void EndGameCampaign(bool isWin)
    {
        SetTitle(isWin);

        if (isWin) textEndGame.text = campaignWinText;
        else textEndGame.text = campaignLoseText;

        returnButton.SetActive(true);
    }

    public void EndGameSandBox(bool isWin)
    {
        SetTitle(isWin);

        if (isWin) textEndGame.text = sandboxWinText;
        else textEndGame.text = sandboxLoseText;

        resetButton.SetActive(true);
        settingsButton.SetActive(true);
        mainMenuButton.SetActive(true);
    }


    private void SetTitle(bool isWin)
    {
        if (isWin) titleEndGame.text = "Победа!";
        else titleEndGame.text = "Поражение..";
    }
}
