using UnityEngine;
using Zenject;

public class LoadData : MonoBehaviour
{
    [Inject] private ProgressPlayer player;
    [Inject] private ProgressEnemy1 enemy1;
    [Inject] private ProgressEnemy2 enemy2;
    [Inject] private ProgressEnemy3 enemy3;

    [SerializeField] private bool isSandbox;
    [SerializeField] private bool isCampaign;

    void Start()
    {
        ResetProgressEnemy();

        if (isSandbox)
        {
            player.LoadDataSandbox();
            return;
        }
        else if (isCampaign)
        {
            player.LoadDataCampaign();
            return;
        }
    }

    public void ResetProgressEnemy()
    {
        enemy1.ResetProgress();
        enemy2.ResetProgress();
        enemy3.ResetProgress();
    }
}
