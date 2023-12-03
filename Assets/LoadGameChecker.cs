using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class LoadGameChecker : MonoBehaviour
{
    [Inject] private ProgressPlayer player;

    [SerializeField] private GameObject loadGame;

    void Start()
    {
        CheckGameRunning();
    }

    public void NewGame()
    {
        player.NewGame();
        SceneManager.LoadScene(4);

        Debug.Log("New Game starting");
    }

    public void ResetRunning()
    {
        player.ResetFirstRunning();
    }

    private void CheckGameRunning()
    {
        if (!player.isFirstRunning)
        {
            loadGame.GetComponent<Button>().interactable = true;
        }
    }
}
