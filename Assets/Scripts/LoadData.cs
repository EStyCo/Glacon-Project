using UnityEngine;
using Zenject;

public class LoadData : MonoBehaviour
{
    [Inject] private ProgressPlayer player;

    [SerializeField] private bool isSandbox;
    [SerializeField] private bool isCampaign;

    void Start()
    {
        if (isSandbox)
        {
            player.LoadDataSandbox();
            //uiProgress.UpdatePosition();
            return;
        }
        else if (isCampaign)
        {
            player.LoadDataCampaign();
            return;
        }
    }

}
