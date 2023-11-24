using UnityEngine;
using Zenject;

public class ResetDataSandbox : MonoBehaviour
{
    [Inject] private ProgressPlayer player;
    [Inject] private UIProgress uiProgress;

    public void ResetData()
    {
        player.ResetDataSandbox();
        uiProgress.UpdatePosition();
    }
}
