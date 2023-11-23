using UnityEngine;
using Zenject;

public class ResertData : MonoBehaviour
{
    [Inject] private GameManager gameManager;
    [Inject] private UIProgress uiProgress;

    public void ResetData()
    {
        gameManager.ResetData();
        uiProgress.UpdatePosition();
    }
}
