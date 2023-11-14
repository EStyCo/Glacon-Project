using UnityEngine;
using Zenject;

public class ResertData : MonoBehaviour
{
    [Inject] GameManager gameManager;

    public void ResetData()
    {
        gameManager.ResetData();
    }
}
